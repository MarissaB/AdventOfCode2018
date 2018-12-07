using QuickGraph;
using QuickGraph.Algorithms.TopologicalSort;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
        Step starter;
        Step ending;
        string finalResult;

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
                    //GetSteps(sortedStrings);
                    Question1Text.Text += "\r\n" + LastShot(sortedStrings);
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

            foreach (string sort in sorted)
            {
                Part1Logging.Text += sort + "\r\n";
            }
            return sorted;
        }

        private void GetSteps(List<string> sortedInputs)
        {
            Step currentStep = new Step();
            List<Step> beforeSteps = new List<Step>();
            List<Step> afterSteps = new List<Step>();

            foreach (string input in sortedInputs)
            {
                Step after = GetStep(input[36]);
                Step letter = GetStep(input[5]);

                currentStep = letter;
                currentStep.After.Add(after.Letter);

                beforeSteps.Add(letter);
                afterSteps.Add(after);
            }

            GetStarter(beforeSteps, afterSteps);
            GetEnding(beforeSteps, afterSteps);

        }

        private string LastShot(List<string> sortedInputs)
        {
            var graph = new AdjacencyGraph<Letter, Edge<Letter>>();
            List<char> alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
            alphabet.Reverse();
            List<Letter> letterList = new List<Letter>();

            foreach (char c in alphabet)
            {
                letterList.Add(new Letter(c));
            }

            graph.AddVertexRange(letterList);

            foreach (string input in sortedInputs)
            {
                Letter addNew = letterList.Where(l => l.Char == input[5]).First();
                Letter addNew2 = letterList.Where(o => o.Char == input[36]).First();

                graph.AddEdge(new Edge<Letter>(addNew, addNew2));
            }

            var sort = new TopologicalSortAlgorithm<Letter, Edge<Letter>>(graph);
            sort.Compute();

            StringBuilder builder = new StringBuilder();
            foreach (var item in sort.SortedVertices)
            {
                builder.Append(item.ToString());
            }
            var word = builder.ToString();

            // bad == EUKGJYXNIFSTCQWLZMAVPORDBH

            return word;
        }

        public class Letter
        {
            public char Char;

            public Letter(char letter)
            {
                Char = letter;
            }

            public override string ToString()
            {
                return Char.ToString();
            }

        }

        private Step GetStep(char letter)
        {
            int index = stepList.FindIndex(s => s.Letter == letter);

            if (index >= 0) // Check if step already exists
            {
                return stepList[index]; // Return the found step
            }
            else
            {
                Step step = new Step(letter); // Make the new step, then return it
                stepList.Add(step);

                return step;
            }
        }

        private void GetStarter(List<Step> before, List<Step> after)
        {
            foreach (Step step in before)
            {
                if (after.Contains(step))
                {
                    continue;
                }
                else
                {
                    starter = step;
                    break;
                }
            }
        }

        private void GetEnding(List<Step> before, List<Step> after)
        {
            foreach (Step letter in after)
            {
                if (before.Contains(letter))
                {
                    continue;
                }
                else
                {
                    ending = letter;
                    break;
                }
            }
        }


        private void CalculatePart2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
