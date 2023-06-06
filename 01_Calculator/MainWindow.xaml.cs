using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _01_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public string MathStringUp { get; set; }
        //public string MathStringDown { get; set; }
        public string Number { get; set; }
        double res;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Number_Action(object sender, RoutedEventArgs e)
        {
           
            if (Char.IsNumber(Convert.ToChar((sender as Button).Content)) || (sender as Button).Content.ToString()==".")
            {
                if (Up.Text.EndsWith("="))
                {
                    Clear();
                }
                if (Number=="0" && (sender as Button).Content.ToString()!=".")
                {
                    MessageBox.Show("Add . ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if((sender as Button).Content.ToString() == "." && Number.Contains('.'))
                    {
                        MessageBox.Show("There is already a '.' in this number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        Number += (sender as Button).Content.ToString();
                        Down.Text = Number;
                    }
                }
            }
            else
            {
                if (Up.Text.EndsWith("="))
                {
                    Up.Text=Down.Text;
                }
                if (!String.IsNullOrEmpty(Number))
                {
                    Up.Text += Number;
                    Number = "";
                }
                if (string.IsNullOrEmpty(Up.Text))
                {
                    MessageBox.Show("There is no number with which you can perform a mathematical operation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    char[] arr = Up.Text.ToCharArray();
                    if (Char.IsNumber(arr[arr.Length-1]))
                    {
                      Up.Text += (sender as Button).Content.ToString();
                    }
                    else
                    {
                        arr[arr.Length-1]= Convert.ToChar((sender as Button).Content);
                        Up.Text = new string(arr);
                    }
                    
                }
            }
        }

        private void Button_Click_Calculation(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(Number))
            {
                Up.Text += Number;
                Number = "";
                
            }
            else
            {
                Up.Text= Up.Text.Remove(Up.Text.Length - 1);
            }
            DataTable dt = new DataTable();
            var result = dt.Compute(Up.Text, "");
            Up.Text += "=";
            Down.Text = result.ToString();
        }

        private void Button_Click_Del(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.ToString() == "C")
            {
                Clear();
            }
            if ((sender as Button).Content.ToString() == "CE")
            {
                Number = "";
                Down.Text = "";
            }
            if ((sender as Button).Content.ToString() == "X")
            {
                if (Down.Text.Length > 1)
                {
                    Down.Text = Down.Text.Remove(Down.Text.Length - 1);
                    Number = Down.Text;
                }
                else
                {
                    Down.Text = "";
                    Number = Down.Text;
                }
            }
        }
       private void Clear()
        {
            Number = "";
            Up.Text = "";
            Down.Text = "";
        }
    }
}
