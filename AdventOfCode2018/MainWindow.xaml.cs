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
            Day_1 day = new Day_1();
            day.Show();
        }

        private void Day2Button_Click(object sender, RoutedEventArgs e)
        {
            Day_2 day = new Day_2();
            day.Show();
        }

        private void Day3Button_Click(object sender, RoutedEventArgs e)
        {
            Day_3 day = new Day_3();
            day.Show();
        }

        private void Day4Button_Click(object sender, RoutedEventArgs e)
        {
            Day_4 day = new Day_4();
            day.Show();
        }

        private void Day5Button_Click(object sender, RoutedEventArgs e)
        {
            Day_5 day = new Day_5();
            day.Show();
        }

        private void Day7Button_Click(object sender, RoutedEventArgs e)
        {
            Day_7 day = new Day_7();
            day.Show();
        }
    }
}
