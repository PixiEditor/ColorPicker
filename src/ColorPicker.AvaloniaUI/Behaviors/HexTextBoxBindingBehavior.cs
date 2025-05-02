using Avalonia;
using Avalonia.Media;
using ColorPicker.Converters;
using ColorPicker.Models;

namespace ColorPicker.Behaviors;

internal class HexTextBoxBindingBehavior : LostFocusUpdateBindingBehavior
{
    public static readonly StyledProperty<Color> ColorProperty =
        AvaloniaProperty.Register<HexTextBoxBindingBehavior, Color>(
            "Color");

    public Color Color
    {
        get => GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public static readonly StyledProperty<ColorToHexConverter> HexConverterProperty =
        AvaloniaProperty.Register<HexTextBoxBindingBehavior, ColorToHexConverter>(
            "HexConverter");

    public ColorToHexConverter HexConverter
    {
        get => GetValue(HexConverterProperty);
        set => SetValue(HexConverterProperty, value);
    }

    protected override void OnSubmitValue(string oldValue, string newValue)
    {
        object value = HexConverter.ConvertBack(newValue, typeof(string), null, null);
        if (value is not Avalonia.Media.Color color)
        {
            Text = oldValue;
            return;
        }

        Color = color;
    }
}
