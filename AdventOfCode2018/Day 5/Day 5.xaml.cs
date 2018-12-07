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
                    Question1Text.Text += "Final Polymer: " + ReactPolymer(inputString).Length;
                }
                catch
                {
                    MessageBox.Show("Failed to import polymer file.", "ERROR: INVALID FILE", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string ReadInput(string fileName)
        {
            string line = File.ReadAllText(Path.ChangeExtension(fileName, ".txt"));
            Part1Logging.Text += line + "\r\n";

            return line;
        }

        private string ReactPolymer(string polymer)
        {
            do
            {
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

            for (int count = 0; count < polymerUnits.Length; count++)
            {
                char current = polymerUnits[count];

                if (count + 1 == polymerUnits.Length)
                {
                    result += current;
                    return result;
                }

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
                    else
                    {
                        count++;
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
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] alphaUpper = alpha.ToUpper().ToCharArray();
            char[] alphaLower = alpha.ToLower().ToCharArray();
            string winningLetter = string.Empty;
            int shortestCount = inputString.Length;

            for (int count = 0; count < alpha.Length; count++)
            {
                string letterUpper = alphaUpper[count].ToString();
                string letterLower = alphaLower[count].ToString();
                string letters = letterUpper + "/" + letterLower;

                string testPolymer = inputString.Replace(letterUpper, "");
                testPolymer = testPolymer.Replace(letterLower, "");

                int testCount = ReactPolymer(testPolymer).Length;

                Thread.Sleep(100);
                Part2Logging.Text += "Testing " + letters + ". Result: " + testCount + "\r\n";
                DoEvents();
                

                if (testCount < shortestCount)
                {
                    winningLetter = letters;
                    shortestCount = testCount;
                }

            }

            Part2Logging.Text += "Best result: " + shortestCount + " from " + winningLetter;
        }

        private void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame(true);
            Dispatcher.CurrentDispatcher.BeginInvoke
            (
            DispatcherPriority.Background,
            (SendOrPostCallback)delegate (object arg)
            {
                var f = arg as DispatcherFrame;
                f.Continue = false;
            },
            frame
            );
            Dispatcher.PushFrame(frame);
        }
    }
}
