using System.Windows;
using System.Windows.Input;
using Avalonia;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace ColorPicker
{
    public partial class ColorDisplay : DualPickerControlBase
    {
        public static readonly StyledProperty<double> CornerRadiusProperty = AvaloniaProperty.Register<ColorDisplay, double>(
            nameof(CornerRadius), 0);
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }


        public ColorDisplay() : base()
        {
            InitializeComponent();
        }

        private void SwapButton_Click(object sender, RoutedEventArgs e)
        {
            SwapColors();
        }

        private void HintColor_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                SetMainColorFromHintColor();
            }
        }
    }
}
