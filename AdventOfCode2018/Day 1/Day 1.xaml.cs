using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace AdventOfCode2018
{
    /// <summary>
    /// Interaction logic for Day_1.xaml
    /// </summary>
    public partial class Day_1 : Window
    {
        public Day_1()
        {
            InitializeComponent();
        }

        private string[] inputNumbers;
        int currentFrequency = 0;
        private List<int> finalFrequencies = new List<int>();
        bool foundDouble = false;

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
                    inputNumbers = ReadInput(filename);
                    ProcessFrequencyChanges();
                    Question1Text.Text += " " + currentFrequency;
                }
                catch
                {
                    MessageBox.Show("Failed to import frequencies file.", "ERROR: INVALID FILE", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string[] ReadInput(string fileName)
        {
            // We change file extension here to make sure it's a .tsv file.
            string[] lines = File.ReadAllLines(Path.ChangeExtension(fileName, ".txt"));

            return lines;
        }

        private void ProcessFrequencyChanges()
        {
            string loggingText = string.Empty;

            foreach (string frequencyChange in inputNumbers)
            {
                int startingFrequency = currentFrequency;
                currentFrequency = startingFrequency + Convert.ToInt32(frequencyChange);
                FindDuplicateFrequency(currentFrequency);
                if (!foundDouble)
                {
                    finalFrequencies.Add(currentFrequency);
                }
                else
                {
                    return;
                }

                loggingText += LogFrequencyChange(startingFrequency, frequencyChange, currentFrequency);
            }

            Part1Logging.Text = loggingText;
        }

        private string LogFrequencyChange(int starting, string change, int final)
        {
            string log = "Current frequency " + starting + ", change of " + change + "; resulting frequency " + final + ".\r\n";
            return log;
        }

        private void FindDuplicateFrequency(int frequency)
        {
            if (finalFrequencies.Contains(frequency))
            {
                foundDouble = true;
                Part2Logging.Text += "Found frequency " + frequency + " twice!";
                Question2Text.Text += frequency;
                return;
            }
        }

        private void CalculatePart2_Click(object sender, RoutedEventArgs e)
        {
            do
            {
                Thread.Sleep(100);
                Part2Logging.Text += "No match found. Processing again.\r\n";
                DoEvents();
                ProcessFrequencyChanges();

            } while (!foundDouble);
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