using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _04_Schulte_table
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int temp = 0;
        DispatcherTimer timer = new DispatcherTimer();
        Random random = new Random();
        List<int> numbers;
        Button[] buttons;
        public MainWindow()
        {
            InitializeComponent();
            numbers = Enumerable.Range(1, 24).ToList();
            buttons = Numbers.Children.OfType<Button>().ToArray();
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            timer.Tick += Timer_Tick; 
            foreach (Button button in buttons)
            {
                button.IsEnabled = false;
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
           
            if (Progress_time.Value == Progress_time.Maximum)
            {
                timer.Stop();
                if (MessageBox.Show("Time is up! YOU LOST!", "Game Over", MessageBoxButton.OK)==MessageBoxResult.OK)
                {
                    foreach (Button button in buttons)
                    {
                        button.IsEnabled = false;
                    }
                }
            }
            else
            {
                Progress_time.Value++;
                label.Content = $"Time: {Progress_time.Value} sec";
            }
        }

        void Mix()
        {
            temp = 0;
            foreach (Button button in buttons)
            {
                button.Background = new SolidColorBrush(Colors.WhiteSmoke);
                Progress_time.Value= Progress_time.Minimum;
                label.Content = "Time: 0 sec";
            }
            int n = numbers.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                int temp = numbers[k];
                numbers[k] = numbers[n];
                numbers[n] = temp;
            }
            int i = 0;
            foreach (Button button in buttons)
            {
                button.Content = numbers[i].ToString();
                button.FontSize = 20;
                i++;
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Mix();
            foreach (Button button in buttons)
            {
                button.IsEnabled = true;
            }
            Progress_time.Maximum = Seconds.Value;
            timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if((sender as Button).Content.ToString()==(temp+1).ToString())
            {
                (sender as Button).Background = new SolidColorBrush(Colors.LightGreen);
                temp++;
                if (temp==24)
                {
                    timer.Stop();
                    if (MessageBox.Show($"YOU WIN! {label.Content}", "Game Over", MessageBoxButton.OK) == MessageBoxResult.OK)
                    {
                        foreach (Button button in buttons)
                        {
                            button.IsEnabled = false;
                        }
                    }
                }
            }        
        }
    }
}
