using System.Windows;
using System.Windows.Input;

namespace ColorPicker
{
    public partial class ColorDisplay : DualPickerControlBase
    {

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
