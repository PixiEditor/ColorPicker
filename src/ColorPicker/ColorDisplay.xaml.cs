using System.Windows;
using System.Windows.Input;

namespace ColorPicker
{
    public partial class ColorDisplay : DualPickerControlBase
    {
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(double), typeof(ColorDisplay)
                , new PropertyMetadata(0d));


        public ColorDisplay()
        {
            InitializeComponent();
        }

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private void SwapButton_Click(object sender, RoutedEventArgs e)
        {
            SwapColors();
        }

        private void HintColor_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetMainColorFromHintColor();
        }
    }
}