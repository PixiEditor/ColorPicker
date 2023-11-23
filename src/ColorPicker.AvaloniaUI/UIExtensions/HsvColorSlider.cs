using Avalonia;
using Avalonia.Media;
using ColorPicker.Models;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.UIExtensions;

internal class HsvColorSlider : PreviewColorSlider
{
    public static readonly StyledProperty<string> SliderHsvTypeProperty =
        AvaloniaProperty.Register<HsvColorSlider, string>(
            nameof(SliderHsvType));

    protected override bool RefreshGradient => SliderHsvType != "H";

    public string SliderHsvType
    {
        get => GetValue(SliderHsvTypeProperty);
        set => SetValue(SliderHsvTypeProperty, value);
    }

    protected override void GenerateBackground()
    {
        if (SliderHsvType == "H")
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
        switch (SliderHsvType)
        {
            case "H":
            {
                var rgb = RgbHelper.HsvToRgb(value, 1.0, 1.0);
                return Color.FromArgb(255, (byte)(rgb.R * 255), (byte)(rgb.G * 255), (byte)(rgb.B * 255));
            }
            case "S":
            {
                var rgb =
                    RgbHelper.HsvToRgb(CurrentColorState.HSV_H, value / 255.0, CurrentColorState.HSV_V);
                return Color.FromArgb(255, (byte)(rgb.R * 255), (byte)(rgb.G * 255), (byte)(rgb.B * 255));
            }
            case "V":
            {
                var rgb =
                    RgbHelper.HsvToRgb(CurrentColorState.HSV_H, CurrentColorState.HSV_S, value / 255.0);
                return Color.FromArgb(255, (byte)(rgb.R * 255), (byte)(rgb.G * 255), (byte)(rgb.B * 255));
            }
            default:
                return Color.FromArgb((byte)(CurrentColorState.A * 255), (byte)(CurrentColorState.RGB_R * 255),
                    (byte)(CurrentColorState.RGB_G * 255), (byte)(CurrentColorState.RGB_B * 255));
        }
    }
}