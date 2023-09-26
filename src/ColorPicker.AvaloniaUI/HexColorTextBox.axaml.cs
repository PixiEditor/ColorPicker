using Avalonia;

namespace ColorPicker
{
    public partial class HexColorTextBox : PickerControlBase
    {
        public static readonly StyledProperty<bool> ShowAlphaProperty = AvaloniaProperty.Register<HexColorTextBox, bool>(
            nameof(ShowAlpha), true);

        public bool ShowAlpha
        {
            get => GetValue(ShowAlphaProperty);
            set => SetValue(ShowAlphaProperty, value);
        }

        public HexColorTextBox() : base()
        {
            InitializeComponent();
        }

        private void ColorToHexConverter_OnShowAlphaChange(object sender, System.EventArgs e)
        {
            //TODO: Not found, only line below, last two were already commented out
            //textbox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            //InvalidateProperty(SelectedColorProperty);
            //Color.RaisePropertyChanged(nameof(Color.RGB_R));
        }
    }
}
