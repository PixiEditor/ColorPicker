using System.Windows;

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
    }
}
