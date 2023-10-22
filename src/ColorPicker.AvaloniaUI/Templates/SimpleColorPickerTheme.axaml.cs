using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace ColorPicker.AvaloniaUI.Templates;

public class SimpleColorPickerTheme : Styles
{
    public SimpleColorPickerTheme()
    {
        AvaloniaXamlLoader.Load(this);
    }
}