using Avalonia.Controls;
using ColorPicker.Models;

namespace ColorPickerDemo.AvaloniaUI.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        combobox.SelectionChanged += (sender, args) =>
        {
            square_picker.PickerType = combobox.SelectedIndex == 0 ? PickerType.HSV : PickerType.HSL;
        };
    }
}