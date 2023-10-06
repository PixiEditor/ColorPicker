using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace ColorPicker.AvaloniaUI.Templates;

public class ColorPickerTheme : Styles
{
    public ColorPickerTheme()
    {
        AvaloniaXamlLoader.Load(this);
    }
}