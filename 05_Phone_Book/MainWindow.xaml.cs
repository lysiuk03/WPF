using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xml.Linq;
using System.Xml.Serialization;
using Path = System.IO.Path;

namespace _05_Phone_Book
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Contact> contacts = null;
        public MainWindow()
        {
            InitializeComponent();
            contacts = new ObservableCollection<Contact>()
            {
                new Contact() {Name = "Bob", Surname="Daret", Phone= "0675847835",Country="Ukraine"},
                new Contact() {Name = "Ruby", Surname="Smidf", Phone= "0684759345",Country="Ukraine"},
                new Contact() {Name = "Sem", Surname="Deryb", Phone= "0574957384",Country="Ukraine"}
            };
            list.Items.Clear();
            list.ItemsSource = contacts;
            list.DisplayMemberPath = nameof(Contact.FullInfo);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Name_.Text) && !string.IsNullOrEmpty(Surname_.Text) && !string.IsNullOrEmpty(Phone_.Text) && !string.IsNullOrEmpty(Country_.Text))
            {
                contacts.Add(new Contact() { Name = Name_.Text, Surname = Surname_.Text, Phone = Phone_.Text, Country = Country_.Text });
            }
            else
            {
                MessageBox.Show("Enter all data");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (list.SelectedItem != null)
            {
                Name_.Text = "";
                Surname_.Text = "";
                Phone_.Text = "";
                Country_.Text = "";
                contacts.Remove(list.SelectedItem as Contact);
            }
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list.SelectedItem != null)
            {
                Name_.Text = ((Contact)list.SelectedItem).Name;
                Surname_.Text = ((Contact)list.SelectedItem).Surname;
                Phone_.Text = ((Contact)list.SelectedItem).Phone;
                Country_.Text = ((Contact)list.SelectedItem).Country;
            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (list.SelectedItem != null)
            {
                Contact p = list.SelectedItem as Contact;
                if (!string.IsNullOrEmpty(Name_.Text) && !string.IsNullOrEmpty(Surname_.Text) && !string.IsNullOrEmpty(Phone_.Text) && !string.IsNullOrEmpty(Country_.Text))
                {
                    p.Name = Name_.Text;
                    p.Surname = Surname_.Text;
                    p.Phone = Phone_.Text;
                    p.Country = Country_.Text;
                }
                else
                {
                    MessageBox.Show("Enter all data");
                }
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Contact>));
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "phonebook.xml");

            if (File.Exists(filePath))
            {
                using (TextReader reader = new StreamReader(filePath))
                {
                    contacts.Clear();
                    contacts = (ObservableCollection<Contact>)serializer.Deserialize(reader);
                }
                list.ItemsSource = contacts;
            }
            else
            {
                MessageBox.Show("File not found!");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Contact>));

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "phonebook.xml");

            using (TextWriter writer = new StreamWriter(File.Create(filePath)))
            {
                serializer.Serialize(writer, contacts);
            }
        }
    }
    public class Contact : INotifyPropertyChanged
    {
        private string _name;
        private string _surname;
        private string _phone;
        private string _country;
        public string Name
        {
            get => _name;
            set 
            {
                this._name = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullInfo));
            }
        }
        public string Surname
        {
            get => _surname;
            set 
            {
                this._surname = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullInfo));
            }
        }
        public string Phone
        {
            get => _phone;
            set
            {
                this._phone = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullInfo));
            }
        }
        public string Country
        {
            get => _country;
            set
            {
                this._country = value;
            }
        }
        public string FullInfo => Name + " " + Surname+" : "+Phone;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public override string ToString()
        {
            return $"{Name} {Surname} : {Phone}";
        }
    }
}
