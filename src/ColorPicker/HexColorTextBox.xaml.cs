using System.Windows;

namespace ColorPicker
{
    public partial class HexColorTextBox : PickerControlBase
    {
        public static readonly DependencyProperty ShowAlphaProperty =
            DependencyProperty.Register(nameof(ShowAlpha), typeof(bool), typeof(HexColorTextBox),
                new PropertyMetadata(true));


        public bool ShowAlpha
        {
            get => (bool)GetValue(ShowAlphaProperty);
            set => SetValue(ShowAlphaProperty, value);
        }

        public HexColorTextBox() : base()
        {
            InitializeComponent();
        }
    }
}
