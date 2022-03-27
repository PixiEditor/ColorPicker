using System.Windows;
using System.Windows.Input;

namespace ColorPicker
{
    public partial class ColorDisplay : DualPickerControlBase
    {
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(double), typeof(ColorDisplay)
                , new PropertyMetadata(0d));


        public ColorDisplay() : base()
        {
            InitializeComponent();
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
