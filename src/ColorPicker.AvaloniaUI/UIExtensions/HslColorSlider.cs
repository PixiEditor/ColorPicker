using Avalonia;
using Avalonia.Media;
using ColorPicker.Models;
using ColorPicker.Models.ColorSpaces;

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
                var rgb = RgbHelper.HslToRgb(value, 1.0, 0.5);
                return Color.FromArgb(255, (byte)(rgb.R * 255), (byte)(rgb.G * 255), (byte)(rgb.B * 255));
            }
            case "S":
            {
                var rgb =
                    RgbHelper.HslToRgb(CurrentColorState.HSL_H, value / 255.0, CurrentColorState.HSL_L);
                return Color.FromArgb(255, (byte)(rgb.R * 255), (byte)(rgb.G * 255), (byte)(rgb.B * 255));
            }
            case "L":
            {
                var rgb =
                    RgbHelper.HslToRgb(CurrentColorState.HSL_H, CurrentColorState.HSL_S, value / 255.0);
                return Color.FromArgb(255, (byte)(rgb.R * 255), (byte)(rgb.G * 255), (byte)(rgb.B * 255));
            }
            default:
                return Color.FromArgb(255, (byte)(CurrentColorState.RGB_R * 255), (byte)(CurrentColorState.RGB_G * 255),
                    (byte)(CurrentColorState.RGB_B * 255));
        }
    }
}