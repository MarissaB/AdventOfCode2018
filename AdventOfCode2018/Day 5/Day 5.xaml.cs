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
                    /*sortedStrings = SortInput(inputStrings);
                    GetGuards(sortedStrings);
                    Guard sleepyGuard = FindSleepyGuard(guards);
                    Question1Text.Text */
                }
                catch
                {
                    MessageBox.Show("Failed to import guard observations file.", "ERROR: INVALID FILE", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void CalculatePart2_Click(object sender, RoutedEventArgs e)
        {

        }
}
