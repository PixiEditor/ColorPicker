using System;
using System.Windows;
using System.Windows.Controls;

namespace ColorPicker
{
    public partial class HexColorTextBox : PickerControlBase
    {
        public static readonly DependencyProperty ShowAlphaProperty =
            DependencyProperty.Register(nameof(ShowAlpha), typeof(bool), typeof(HexColorTextBox),
                new PropertyMetadata(true));

        public HexColorTextBox()
        {
            InitializeComponent();
        }

        public bool ShowAlpha
        {
            get => (bool)GetValue(ShowAlphaProperty);
            set => SetValue(ShowAlphaProperty, value);
        }

        private void ColorToHexConverter_OnShowAlphaChange(object sender, EventArgs e)
        {
            textbox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            //InvalidateProperty(SelectedColorProperty);
            //Color.RaisePropertyChanged(nameof(Color.RGB_R));
        }
    }
}