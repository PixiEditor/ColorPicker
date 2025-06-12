using Avalonia;
using Avalonia.Media;

namespace ColorPicker.UIExtensions;

internal class RgbColorSlider : PreviewColorSlider
{
    public static readonly StyledProperty<string> SliderArgbTypeProperty =
        AvaloniaProperty.Register<RgbColorSlider, string>(
            nameof(SliderArgbType));

    public string SliderArgbType
    {
        get => GetValue(SliderArgbTypeProperty);
        set => SetValue(SliderArgbTypeProperty, value);
    }

    protected override void GenerateBackground()
    {
        var colorStart = GetColorForSelectedArgb(0);
        var colorEnd = GetColorForSelectedArgb(255);
        LeftCapColor.Color = colorStart;
        RightCapColor.Color = colorEnd;
        BackgroundGradient = new GradientStops
        {
            new(colorStart, 0.0),
            new(colorEnd, 1)
        };
    }

    private Color GetColorForSelectedArgb(int value)
    {
        var a = (byte)(CurrentColorState.A * 255);
        var r = (byte)(CurrentColorState.RGB_R * 255);
        var g = (byte)(CurrentColorState.RGB_G * 255);
        var b = (byte)(CurrentColorState.RGB_B * 255);
        switch (SliderArgbType)
        {
            case "A": return Color.FromArgb((byte)value, r, g, b);
            case "R": return Color.FromArgb(255, (byte)value, g, b);
            case "G": return Color.FromArgb(255, r, (byte)value, b);
            case "B": return Color.FromArgb(255, r, g, (byte)value);
            default: return Color.FromArgb(a, r, g, b);
        }

        ;
    }
}
