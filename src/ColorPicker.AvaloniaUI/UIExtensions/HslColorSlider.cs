using Avalonia;
using Avalonia.Media;
using ColorPicker.Models;

namespace ColorPicker.UIExtensions;

internal class HslColorSlider : PreviewColorSlider
{
    public static readonly StyledProperty<string> SliderHslTypeProperty =
        AvaloniaProperty.Register<HslColorSlider, string>(
            nameof(SliderHslType));

    protected override bool RefreshGradient => SliderHslType != "H";

    public string SliderHslType
    {
        get => GetValue(SliderHslTypeProperty);
        set => SetValue(SliderHslTypeProperty, value);
    }

    protected override void GenerateBackground()
    {
        if (SliderHslType == "H")
        {
            var colorStart = GetColorForSelectedArgb(0);
            var colorEnd = GetColorForSelectedArgb(360);
            LeftCapColor.Color = colorStart;
            RightCapColor.Color = colorEnd;
            BackgroundGradient = new GradientStops
            {
                new(colorStart, 0),
                new(GetColorForSelectedArgb(60), 1 / 6.0),
                new(GetColorForSelectedArgb(120), 2 / 6.0),
                new(GetColorForSelectedArgb(180), 0.5),
                new(GetColorForSelectedArgb(240), 4 / 6.0),
                new(GetColorForSelectedArgb(300), 5 / 6.0),
                new(colorEnd, 1)
            };
            return;
        }

        if (SliderHslType == "L")
        {
            var colorStart = GetColorForSelectedArgb(0);
            var colorEnd = GetColorForSelectedArgb(255);
            LeftCapColor.Color = colorStart;
            RightCapColor.Color = colorEnd;
            BackgroundGradient = new GradientStops
            {
                new(colorStart, 0),
                new(GetColorForSelectedArgb(128), 0.5),
                new(colorEnd, 1)
            };
            return;
        }

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
    }

    private Color GetColorForSelectedArgb(int value)
    {
        switch (SliderHslType)
        {
            case "H":
            {
                var rgbtuple = ColorSpaceHelper.HslToRgb(value, 1.0, 0.5);
                double r = rgbtuple.Item1, g = rgbtuple.Item2, b = rgbtuple.Item3;
                return Color.FromArgb(255, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
            }
            case "S":
            {
                var rgbtuple =
                    ColorSpaceHelper.HslToRgb(CurrentColorState.HSL_H, value / 255.0, CurrentColorState.HSL_L);
                double r = rgbtuple.Item1, g = rgbtuple.Item2, b = rgbtuple.Item3;
                return Color.FromArgb(255, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
            }
            case "L":
            {
                var rgbtuple =
                    ColorSpaceHelper.HslToRgb(CurrentColorState.HSL_H, CurrentColorState.HSL_S, value / 255.0);
                double r = rgbtuple.Item1, g = rgbtuple.Item2, b = rgbtuple.Item3;
                return Color.FromArgb(255, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
            }
            default:
                return Color.FromArgb(255, (byte)(CurrentColorState.RGB_R * 255), (byte)(CurrentColorState.RGB_G * 255),
                    (byte)(CurrentColorState.RGB_B * 255));
        }
    }
}
