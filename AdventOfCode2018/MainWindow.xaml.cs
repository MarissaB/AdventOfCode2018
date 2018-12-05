using System.Windows;

namespace AdventOfCode2018
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Day1Button_Click(object sender, RoutedEventArgs e)
        {
            Day_1 day1 = new Day_1();
            day1.Show();
        }

        private void Day2Button_Click(object sender, RoutedEventArgs e)
        {
            Day_2 day2 = new Day_2();
            day2.Show();
        }

        private void Day3Button_Click(object sender, RoutedEventArgs e)
        {
            Day_3 day3 = new Day_3();
            day3.Show();
        }

        private void Day4Button_Click(object sender, RoutedEventArgs e)
        {
            Day_4 day4 = new Day_4();
            day4.Show();
        }

        private void Day5Button_Click(object sender, RoutedEventArgs e)
        {
            Day_5 day5 = new Day_5();
            day5.Show();
        }
    }
}
