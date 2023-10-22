using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace ColorPicker.AvaloniaUI.Templates;

public class PixiPerfectColorPickerTheme : Styles
{
    public PixiPerfectColorPickerTheme()
    {
        AvaloniaXamlLoader.Load(this);
    }
}