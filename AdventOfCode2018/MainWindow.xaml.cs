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
    }
}
