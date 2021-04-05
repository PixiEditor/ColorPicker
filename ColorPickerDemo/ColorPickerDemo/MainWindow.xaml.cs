using System.Windows;

namespace ColorPickerDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            combobox.SelectionChanged += (sender, args) =>
            {
                square_picker.PickerType = combobox.SelectedIndex == 0 ? ColorPicker.UserControls.PickerType.HSV : ColorPicker.UserControls.PickerType.HSL;
            };
        }
    }
}
