using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace AdventOfCode2018
{
    /// <summary>
    /// Interaction logic for Day_3.xaml
    /// </summary>
    public partial class Day_3 : Window
    {
        public Day_3()
        {
            InitializeComponent();
        }

        private List<Claim> allClaims = new List<Claim>();

        private void LoadInput_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                // Set filter for file extension and default file extension 
                DefaultExt = ".txt",
                Filter = "Text Files (*.txt)|*.txt"
            };

            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                try
                {
                    allClaims = ReadInput(filename);
                }
                catch
                {
                    MessageBox.Show("Failed to import claim IDs file.", "ERROR: INVALID FILE", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CalculatePart1_Click(object sender, RoutedEventArgs e)
        {
            Question1Text.Text += FindOverlaps(allClaims);
        }

        private List<Claim> ReadInput(string fileName)
        {
            List<Claim> claims = new List<Claim>();
            string[] lines = File.ReadAllLines(Path.ChangeExtension(fileName, ".txt"));

            foreach (string line in lines)
            {
                Part1Logging.Text += line + "\r\n";

                var nums = line.Split(
                    new[] { '#', '@', ',', ':', 'x', ' ' },
                    StringSplitOptions.RemoveEmptyEntries
                ).Select(int.Parse).ToList();

                claims.Add(new Claim(
                    nums[0], // id
                    nums[1], // left
                    nums[2], // top
                    nums[3], // width
                    nums[4] // height
                ));
            }

            return claims;
        }

        private int FindOverlaps(List<Claim> claims) // this takes well over an hour to run to completion - could use some streamlining
        {
            List<Tuple<int, int>> totalCovered = new List<Tuple<int, int>>();
            List<Tuple<int, int>> overlappingCoords = new List<Tuple<int, int>>();

            foreach (Claim claim in claims)
            {
                for (int columnCount = claim.Left; columnCount < claim.Width + claim.Left;)
                {
                    for (int rowCount = claim.Top; rowCount < claim.Length + claim.Top;)
                    {
                        Tuple<int, int> coord = new Tuple<int, int>(columnCount, rowCount);
                        if (totalCovered.Contains(coord))
                        {
                            overlappingCoords.Add(coord);
                        }
                        else
                        {
                            totalCovered.Add(coord);
                        }
                        rowCount++;
                    }
                    columnCount++;
                }
            }

            // It's possible to find a coord in several claims, so cut out duplicates in the overlapping list
            int overlap = overlappingCoords.Select(o => o).Distinct().Count(); 

            return overlap;
        }

        private void CalculatePart2_Click(object sender, RoutedEventArgs e)
        {
            Question2Text.Text += "Claim ID: #" + FindNoOverlap(allClaims).ID;
        }

        private Claim FindNoOverlap(List<Claim> claims) // this takes well over an hour to run to completion - could use some streamlining
        {
            Claim noOverlapClaim = new Claim();
            List<Tuple<int, int>> totalCovered = new List<Tuple<int, int>>();
            List<Tuple<int, int>> overlappingCoords = new List<Tuple<int, int>>();
            List<Claim> nonOverlappingClaims = new List<Claim>();

            claims = claims.OrderByDescending(c => c.Left).ToList();

            foreach (Claim claim in claims)
            {
                for (int columnCount = claim.Left; columnCount < claim.Width + claim.Left;)
                {
                    for (int rowCount = claim.Top; rowCount < claim.Length + claim.Top;)
                    {
                        Tuple<int, int> coord = new Tuple<int, int>(columnCount, rowCount);
                        claim.CoordinatesList.Add(coord);
                        if (totalCovered.Contains(coord))
                        {
                            claim.Overlap = true; // If it has ANY overlapping coords, boot it from the list early on
                            overlappingCoords.Add(coord);
                        }
                        else
                        {
                            totalCovered.Add(coord);
                        }
                        rowCount++;
                    }
                    columnCount++;
                }

                if (claim.Overlap == false)
                {
                    nonOverlappingClaims.Add(claim);
                }
            }
            overlappingCoords = overlappingCoords.Select(o => o).Distinct().ToList();

            foreach (Claim nonOverlappingClaim in nonOverlappingClaims) // Now skim the final list for overlapping coords
            {
                Part2Logging.Text += "Scanning claim: " + nonOverlappingClaim.ID + "...\r\n";
                if (CheckOverlap(nonOverlappingClaim, overlappingCoords) == false)
                {
                    Part2Logging.Text += "It's non-overlapping and has unique coords!";
                    noOverlapClaim = nonOverlappingClaim;
                }
            }

            return noOverlapClaim;
        }

        private bool CheckOverlap(Claim claim, List<Tuple< int, int>> overlapping)
        {
            foreach (Tuple<int, int> coord in claim.CoordinatesList)
            {
                if (overlapping.Contains(coord))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
