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
    /// Interaction logic for Day_2.xaml
    /// </summary>
    public partial class Day_2 : Window
    {
        public Day_2()
        {
            InitializeComponent();
        }

        private string[] inputStrings;
        private List<string> countOf2 = new List<string>();
        private List<string> countOf3 = new List<string>();
        private int matchedIndex = 0;

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
                    inputStrings = ReadInput(filename);
                    ParsePackageIDs();
                    Question1Text.Text += countOf2.Count + " x " + countOf3.Count + " = " + countOf2.Count*countOf3.Count;
                }
                catch
                {
                    MessageBox.Show("Failed to import package IDs file.", "ERROR: INVALID FILE", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string[] ReadInput(string fileName)
        {
            string[] lines = File.ReadAllLines(Path.ChangeExtension(fileName, ".txt"));

            return lines;
        }

        private void ParsePackageIDs()
        {
            foreach (string package in inputStrings)
            {
                Part1Logging.Text += "Package ID:  " + package + "\r\n";
                char[] characters = package.ToCharArray();
                bool count2 = false;
                bool count3 = false;

                foreach (char character in characters)
                {
                    int count = characters.Where(c => c == character).Count();
                    if (count == 2)
                    {
                        count2 = true;
                    }
                    if (count == 3)
                    {
                        count3 = true;
                    }
                }

                if (count2)
                {
                    countOf2.Add(package);
                }
                if (count3)
                {
                    countOf3.Add(package);
                }
            }
        }

        private void CalculatePart2_Click(object sender, RoutedEventArgs e)
        {
            foreach (string package in inputStrings)
            {
                foreach (string package2 in inputStrings)
                {
                    if (package != package2 && ArePackagesSimilar(package, package2))
                    {
                        Part2Logging.Text = "Found a match! \r\n" + package + "\r\n" + package2;
                        Question2Text.Text += package.Remove(matchedIndex, 1);
                        return;
                    }
                }
            }
        }

        private bool ArePackagesSimilar(string package1, string package2)
        {
            char[] characters1 = package1.ToCharArray();
            char[] characters2 = package2.ToCharArray();
            List<bool> matches = new List<bool>();

            for (int count = 0; count < package1.Length; count++)
            {
                bool holder;
                if (characters1[count] == characters2[count])
                {
                    holder = true;
                    matches.Add(holder);
                }
                else
                {
                    holder = false;
                    matches.Add(holder);
                }
            }

            if (matches.Where(m => m == true).Count() != package1.Length-1) // need to match all but one letter
            {
                return false;
            }
            else
            {
                matchedIndex = matches.IndexOf(false);
                return true;
            }
        }
    }
}
