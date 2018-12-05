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
    /// Interaction logic for Day_5.xaml
    /// </summary>
    public partial class Day_5 : Window
    {
        public Day_5()
        {
            InitializeComponent();
        }

        private string inputString;

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
                    inputString = ReadInput(filename);
                    Part1Logging.Text += "Final Polymer: " + ReactPolymer(inputString);
                }
                catch
                {
                    MessageBox.Show("Failed to import polymer file.", "ERROR: INVALID FILE", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string ReadInput(string fileName)
        {
            // We change file extension here to make sure it's a .tsv file.
            string line = File.ReadAllText(Path.ChangeExtension(fileName, ".txt"));
            Part1Logging.Text += line;

            return line;
        }

        private string ReactPolymer(string polymer)
        {
            do
            {
                Part1Logging.Text += polymer + "\r\n";
                polymer = RemoveReactions(polymer);
            }
            while (CheckPolymerForReactions(polymer));

            return polymer;
        }

        private bool CheckPolymerForReactions(string polymer)
        {
            char[] polymerUnits = polymer.ToArray();

            for (int count = 0; count < polymerUnits.Length-1; count++)
            {
                char current = polymerUnits[count];
                char after = polymerUnits[count + 1];

                bool equal = char.ToUpperInvariant(after) == char.ToUpperInvariant(current);

                if (equal) // If this char and the next are the same letter...
                {
                    if (char.IsUpper(current) && char.IsLower(after))
                    {
                        return true; // ...this char and next are opposite cases, which is a reaction.
                    }

                    if (char.IsLower(current) && char.IsUpper(after))
                    {
                        return true; // ...this char and next are opposite cases, which is a reaction.
                    }
                }
            }

            return false;
        }

        private string RemoveReactions(string polymer)
        {
            char[] polymerUnits = polymer.ToArray();
            string result = string.Empty;

            for (int count = 0; count < polymerUnits.Length - 1; count++)
            {
                char current = polymerUnits[count];
                char after = polymerUnits[count + 1];
                bool currentUpper = char.IsUpper(current);
                bool afterUpper = char.IsUpper(after);

                bool equal = char.ToUpperInvariant(after) == char.ToUpperInvariant(current);

                if (equal) // Same letter
                {
                    if (currentUpper == afterUpper) // Same case, no reaction
                    {
                        result += current;
                    }
                }
                else // Different letter, no reaction
                {
                    result += current;
                }

            }

            return result;
        }



        private void CalculatePart2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
