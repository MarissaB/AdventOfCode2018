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
    /// Interaction logic for Day_4.xaml
    /// </summary>
    public partial class Day_4 : Window
    {
        public Day_4()
        {
            InitializeComponent();
        }

        private string[] inputStrings;
        private List<string> sortedStrings;
        private List<Guard> guards = new List<Guard>();
        
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
                    sortedStrings = SortInput(inputStrings);
                    GetGuards(sortedStrings);
                    Guard sleepyGuard = FindSleepyGuard(guards);
                    Question1Text.Text += "Sleepy's ID: " + sleepyGuard.ID + " x " + sleepyGuard.CommonMinute + " = " + sleepyGuard.ID * sleepyGuard.CommonMinute;
                }
                catch
                {
                    MessageBox.Show("Failed to import guard observations file.", "ERROR: INVALID FILE", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string[] ReadInput(string fileName)
        {
            // We change file extension here to make sure it's a .tsv file.
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

        private void GetGuards(List<string> sortedInputs)
        {
            Guard currentGuard = new Guard();
            int sleepBegin = 0;
            foreach (string input in sortedInputs)
            {
                if (input.Contains("Guard"))
                {
                    currentGuard = GetGuard(input);
                }

                if (input.Contains("falls"))
                {
                    sleepBegin = GetMinute(input);
                }

                if (input.Contains("wakes"))
                {
                    currentGuard.AddSleep(sleepBegin, GetMinute(input));
                }
            }
        }

        private int GetMinute(string inputString)
        {
            int minute = Convert.ToInt32(inputString.Substring(15, 2));

            return minute;
        }

        private Guard GetGuard(string input)
        {
            Guard guard = new Guard();
            string[] splitInput = input.Split(null);
            int ID = Convert.ToInt32(splitInput[3].Replace("#", ""));

            int index = guards.FindIndex(g => g.ID == ID);

            if (index >= 0) // Check if guard already exists
            {
                guard = guards[index]; // Return the found guard
            }
            else
            {
                guard = new Guard(input); // Make the new guard, then return it
                guards.Add(guard);
            }

            return guard;
        }

        private Guard FindSleepyGuard(List<Guard> guardList)
        {
            Guard sleepy = new Guard();

            foreach (Guard guard in guardList)
            {
                if (guard.TotalSleepingMinutes > sleepy.TotalSleepingMinutes)
                {
                    sleepy = guard;
                }
            }

            return sleepy;
        }

        private void CalculatePart2_Click(object sender, RoutedEventArgs e)
        {
            Guard dozy = new Guard();

            foreach (Guard guard in guards)
            {
                if (guard.CommonCount > dozy.CommonCount)
                {
                    dozy = guard;
                    Part2Logging.Text += "Most Dozy Guard: ID# " + dozy.ID + ". Fell asleep @ minute " + dozy.CommonMinute + " for " + dozy.CommonCount + " times!\r\n";
                }
            }

            Question2Text.Text += "Dozy's ID: " + dozy.ID + " x " + dozy.CommonMinute + " = " + dozy.ID * dozy.CommonMinute;
        }

        
    }
}
