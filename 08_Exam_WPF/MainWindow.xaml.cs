using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace _08_Exam_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();
        private Button[] buttons;
        bool IsCapsLock = true;
        DispatcherTimer timer;
        int sectime=0;
        
        ViewModel model = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = model;
            buttons = GetGridButtons(key);
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick; ;
            timer.Interval = new TimeSpan(0, 0, 0, 1,0);
            model.Difficulty = 1;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            sectime++;
            Speeds();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (Button button in buttons)
            {
                if (button.Name == e.Key.ToString())
                {
                    button.Opacity = 0.5;
                    if (e.Key.ToString() == "LeftShift" || e.Key.ToString() == "RightShift")
                    {
                        CapitalSymbol();
                        if (IsCapsLock)
                        {
                            CapitalLetters();
                        }
                        else
                        {
                            LowerLetters();
                        }
                    }
                    else if (e.Key.ToString() == "Capital")
                    {
                        if (IsCapsLock)
                        {
                            CapitalLetters();
                            IsCapsLock = false;
                        }
                        else
                        {
                            LowerLetters();
                            IsCapsLock = true;
                        }
                    }
                }
            }
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            foreach (Button button in buttons)
            {
                if (button.Name == e.Key.ToString())
                {
                    button.Opacity = 1;
                    if (e.Key.ToString() == "LeftShift" || e.Key.ToString() == "RightShift")
                    {
                        LowerSymbol();
                        if (!IsCapsLock)
                        {
                            CapitalLetters();
                        }
                        else
                        {
                            LowerLetters();
                        }
                    }
                }
            }
        }
        private Button[] GetGridButtons(Grid grid)
        {
            List<Button> buttonList = new List<Button>();

            foreach (var child in grid.Children)
            {
                if (child is Button button)
                {
                    buttonList.Add(button);
                }
            }

            return buttonList.ToArray();
        }
        void Speeds()
        {
            model.Speed = (int)(input.Text.Length / (double)sectime * 60);
        }
        private void CapitalLetters()
        {
            Q.Content = "Q";
            W.Content = "W";
            E.Content = "E";
            R.Content = "R";
            T.Content = "T";
            Y.Content = "Y";
            U.Content = "U";
            I.Content = "I";
            O.Content = "O";
            P.Content = "P";
            A.Content = "A";
            S.Content = "S";
            D.Content = "D";
            F.Content = "F";
            G.Content = "G";
            H.Content = "H";
            J.Content = "J";
            K.Content = "K";
            L.Content = "L";
            Z.Content = "Z";
            X.Content = "X";
            C.Content = "C";
            V.Content = "V";
            B.Content = "B";
            N.Content = "N";
            M.Content = "M";
        }
        private void LowerLetters()
        {
            Q.Content = "q";
            W.Content = "w";
            E.Content = "e";
            R.Content = "r";
            T.Content = "t";
            Y.Content = "y";
            U.Content = "u";
            I.Content = "i";
            O.Content = "o";
            P.Content = "p";
            A.Content = "a";
            S.Content = "s";
            D.Content = "d";
            F.Content = "f";
            G.Content = "g";
            H.Content = "h";
            J.Content = "j";
            K.Content = "k";
            L.Content = "l";
            Z.Content = "z";
            X.Content = "x";
            C.Content = "c";
            V.Content = "v";
            B.Content = "b";
            N.Content = "n";
            M.Content = "m";
        }
        private void CapitalSymbol()
        {
            Oem3.Content = "~";
            D1.Content = "!";
            D2.Content = "@";
            D3.Content = "#";
            D4.Content = "$";
            D5.Content = "%";
            D6.Content = "^";
            D7.Content = "&";
            D8.Content = "*";
            D9.Content = "(";
            D0.Content = ")";
            OemMinus.Content = "_";
            OemPlus.Content = "+";
            OemOpenBrackets.Content = "{";
            Oem6.Content = "}";
            Oem5.Content = "|";
            Oem1.Content = ":";
            OemQuotes.Content = "\"";
            OemComma.Content = "<";
            OemPeriod.Content = ">";
            OemQuestion.Content = "?";
        }
        private void LowerSymbol()
        {
            Oem3.Content = "`";
            D1.Content = "1";
            D2.Content = "2";
            D3.Content = "3";
            D4.Content = "4";
            D5.Content = "5";
            D6.Content = "6";
            D7.Content = "7";
            D8.Content = "8";
            D9.Content = "9";
            D0.Content = "0";
            OemMinus.Content = "-";
            OemPlus.Content = "=";
            OemOpenBrackets.Content = "[";
            Oem6.Content = "]";
            Oem5.Content = "\\";
            Oem1.Content = ";";
            OemQuotes.Content = "'";
            OemComma.Content = ",";
            OemPeriod.Content = ".";
            OemQuestion.Content = "/";
        }
       
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = false;
            Stop.IsEnabled = true;
            sectime = 0;
            timer.Start();
            GenerateString(model.CaseSensitive, model.Difficulty, model.Difficulty *10);
            input.IsReadOnly = false;
            input.IsEnabled = true;
            input.Focus();
        }
        public void GenerateString(bool inc, int difficulty, int length)
        {
            string loLetters = "abcde fghij klmnop qrstuvwxyz";
            string upLetters = "ABCDEF GHIJKLMNO PQRSTU VWXYZ";
            string numbers = "123 4567 890 ";
            string sumbols = "`-=+_)(*&^ %$#@!~[]} {';\"./,";
            string generate = loLetters;

            if (inc)
            {
                generate += upLetters;
            }
            if (difficulty > 5)
            {
                generate += numbers;
            }
            if (difficulty > 7)
            {
                generate += sumbols;
            }
            string str = "";
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(0, generate.Length);
                str += generate[index];
            }
            model.Str = str;
        }
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            if(input.Text.Equals(model.Str))
            MessageBox.Show($" Good job!\n Time: {sectime} sec\n Fails: {model.Fails}\n Speed: {model.Speed}");
            else
            MessageBox.Show($" You haven't finished the task\n Time: {sectime} sec\n Fails: {model.Fails}\n Speed: {model.Speed}");
            Start.IsEnabled = true;
            Stop.IsEnabled = false;
            model.Fails = 0;
            model.Speed = 0;
            input.Text = "";
            model.Str = "";
            input.IsReadOnly = true;
            input.IsEnabled = false;
            timer.Stop();
            sectime = 0;
        }
        private void input_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string str = model.Str.Substring(0, input.Text.Length);
                if (input.Text.Equals(str))
                {
                    input.Foreground = new SolidColorBrush(Colors.Green);
                    if (input.Text.Length == model.Str.Length)
                    {
                        Stop_Click(sender, e);
                        input.IsReadOnly = true;
                    }
                }
                else
                {
                        model.Fails++;
                    input.Foreground = new SolidColorBrush(Colors.Red);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Too many characters");
            }
        }
    }
    class ViewModel: INotifyPropertyChanged
    {
        private int _difficulty;
        public int Difficulty
        {
            get { return _difficulty; }
            set { _difficulty = value; OnPropertyChanged(); }
        }
        private int _speed;
        public int Speed
        {
            get { return _speed; }
            set { _speed = value; OnPropertyChanged(); }
        }
        private int _fails;
        public int Fails
        {
            get { return _fails; }
            set { _fails = value; OnPropertyChanged(); }
        }
        private string _str;
        public string Str
        {
            get { return _str; }
            set { _str = value; OnPropertyChanged(); }
        }
        private bool _caseSensitive;
        public bool CaseSensitive
        {
            get { return _caseSensitive; }
            set { _caseSensitive = value; OnPropertyChanged(); }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
