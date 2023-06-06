using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Windows;

namespace _06_Color_viewer
{
    class ViewModel : INotifyPropertyChanged
    {
        public Color SelectedColor { get { return Color.FromArgb(A, R, G, B); } }
        private Color delColor;
        public Color DelColor
        {
            get { return delColor; }
            set
            {
                delColor = value;
                OnPropertyChanged();
            }
        }
        private byte _a;
        private byte _r;
        private byte _g;
        private byte _b;

        public byte A
        {
            get { return _a; }
            set { _a = value; OnPropertyChanged(); OnPropertyChanged(nameof(SelectedColor)); }
        }
        public byte R
        {
            get { return _r; }
            set { _r = value; OnPropertyChanged(); OnPropertyChanged(nameof(SelectedColor)); }
        }
        public byte G
        {
            get { return _g; }
            set { _g = value; OnPropertyChanged(); OnPropertyChanged(nameof(SelectedColor)); }
        }
        public byte B
        {
            get { return _b; }
            set { _b = value; OnPropertyChanged(); OnPropertyChanged(nameof(SelectedColor)); }
        }

        private ObservableCollection<Color> _colors;
        public ObservableCollection<Color> Colors
        {
            get { return _colors; }
            set
            {
                _colors = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ViewModel()
        {
            A = 255;
            R = 0;
            G = 0;
            B = 0;
            Colors = new ObservableCollection<Color>();
        }

        public void AddColor()
        {
            if (SelectedColor != null && !Colors.Contains(SelectedColor))
            {
                Colors.Add(SelectedColor);
            }
        }
        public void DeleteColor()
        {
            if (Colors.Contains(DelColor))
            {
                Colors.Remove(DelColor);
                DelColor = SelectedColor;
            }
        }
    }
}
