using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace ColorPicker
{
    public partial class HexColorTextBox : PickerControlBase, INotifyPropertyChanged
    {
        public static readonly DependencyProperty ShowAlphaProperty =
            DependencyProperty.Register(nameof(ShowAlpha), typeof(bool), typeof(HexColorTextBox),
                new PropertyMetadata(true));

        public event PropertyChangedEventHandler PropertyChanged;

        public bool ShowAlpha
        {
            get => (bool)GetValue(ShowAlphaProperty);
            set
            {
                SetValue(ShowAlphaProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextBoxConverter)));
            }
        }

        public IValueConverter TextBoxConverter
        {
            get => ShowAlpha ? (IValueConverter)Resources["ColorToLongHexConverter"] : (IValueConverter)Resources["ColorToShortHexConverter"];
        }

        public HexColorTextBox() : base()
        {
            InitializeComponent();
        }
    }
}
