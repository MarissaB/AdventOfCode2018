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
    /// Interaction logic for Day_7.xaml
    /// </summary>
    public partial class Day_7 : Window
    {
        public Day_7()
        {
            InitializeComponent();
        }

        private string[] inputStrings;
        private List<string> sortedStrings;
        List<Step> stepList;
        char starter;

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
                    stepList = new List<Step>();
                    inputStrings = ReadInput(filename);
                    sortedStrings = SortInput(inputStrings);
                    GetSteps(sortedStrings);
                }
                catch
                {
                    MessageBox.Show("Failed to import steps.", "ERROR: INVALID FILE", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string[] ReadInput(string fileName)
        {
            string[] lines = File.ReadAllLines(Path.ChangeExtension(fileName, ".txt"));

            return lines;
        }

        private List<string> SortInput(string[] rawInputs)
        {
            List<string> sorted = new List<string>();
            sorted = rawInputs.OrderBy(n => n).ToList();

            foreach (string input in sorted)
            {
                Part1Logging.Text += input + "\r\n";
            }

            return sorted;
        }

        private void GetSteps(List<string> sortedInputs)
        {
            Step currentStep = new Step();
            List<char> beforeSteps = new List<char>();
            List<char> afterSteps = new List<char>();

            foreach (string input in sortedInputs)
            {
                char letter = input[36];
                char before = input[5];

                currentStep = GetStep(letter);
                currentStep.Before.Add(before);
                beforeSteps.Add(before);
                afterSteps.Add(letter);
            }

            GetStarter(beforeSteps, afterSteps);
        }

        private void GetStarter(List<char> after, List<char> before)
        {
            foreach (char letter in after)
            {
                if (before.Contains(letter))
                {
                    continue;
                }
                else
                {
                    starter = letter;
                    break;
                }
            }
        }

        private Step GetStep(char letter)
        {
            Step step = new Step();
            int index = stepList.FindIndex(s => s.Letter == letter);

            if (index >= 0) // Check if step already exists
            {
                step = stepList[index]; // Return the found step
            }
            else
            {
                step = new Step(letter); // Make the new step, then return it
                stepList.Add(step);
            }

            return step;
        }

        private List<Step> ParseInput(List<string> rawInput)
        {
            List<Step> steps = new List<Step>();

            return steps;
        }


        private void CalculatePart2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
