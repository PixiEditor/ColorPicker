using System.Windows;

namespace ColorPicker
{
    public partial class HexColorTextBox : PickerControlBase
    {
        public static readonly DependencyProperty ShowAlphaProperty =
            DependencyProperty.Register(nameof(ShowAlpha), typeof(bool), typeof(HexColorTextBox),
                new PropertyMetadata(true, ShowAlphaChangedCallback));

        public bool ShowAlpha
        {
            get => (bool)GetValue(ShowAlphaProperty);
            set => SetValue(ShowAlphaProperty, value);
        }

        public HexColorTextBox() : base()
        {
            InitializeComponent();
        }

        private static void ShowAlphaChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HexColorTextBox source = (HexColorTextBox)d;

        }

        private void ColorToHexConverter_OnShowAlphaChange(object sender, System.EventArgs e)
        {
            Color.RaisePropertyChanged(nameof(SelectedColor));
        }
    }
}
