using ColorPicker.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker.UIExtensions
{
    internal class HsvColorSlider : PreviewColorSlider
    {
        public static readonly DependencyProperty SliderHsvTypeProperty =
            DependencyProperty.Register(nameof(SliderHsvType), typeof(string), typeof(HsvColorSlider),
                new PropertyMetadata(""));

        public HsvColorSlider() : base() { }

        public string SliderHsvType
        {
            get => (string)GetValue(SliderHsvTypeProperty);
            set => SetValue(SliderHsvTypeProperty, value);
        }

        protected override void GenerateBackground()
        {
            if (SliderHsvType == "H")
            {
                Background = new LinearGradientBrush(new GradientStopCollection
                {
                    new GradientStop(GetColorForSelectedArgb(0), 0),
                    new GradientStop(GetColorForSelectedArgb(60), 1/6.0),
                    new GradientStop(GetColorForSelectedArgb(120), 2/6.0),
                    new GradientStop(GetColorForSelectedArgb(180), 0.5),
                    new GradientStop(GetColorForSelectedArgb(240), 4/6.0),
                    new GradientStop(GetColorForSelectedArgb(300), 5/6.0),
                    new GradientStop(GetColorForSelectedArgb(360), 1)
                });
                return;
            }
            Background = new LinearGradientBrush(new GradientStopCollection
            {
                new GradientStop(GetColorForSelectedArgb(0), 0.0),
                new GradientStop(GetColorForSelectedArgb(255), 1)
            });
        }

        private Color GetColorForSelectedArgb(int value)
        {
            switch (SliderHsvType)
            {
                case "H":
                    {
                        var (r, g, b) = HsvHelper.HsvToRgb(value, CurrentColorState.HSV_S, CurrentColorState.HSV_V);
                        return Color.FromArgb((byte)(CurrentColorState.A * 255), (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
                    }
                case "S":
                    {
                        var (r, g, b) = HsvHelper.HsvToRgb(CurrentColorState.HSV_H, value / 255.0, CurrentColorState.HSV_V);
                        return Color.FromArgb((byte)(CurrentColorState.A * 255), (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
                    }
                case "V":
                    {
                        var (r, g, b) = HsvHelper.HsvToRgb(CurrentColorState.HSV_H, CurrentColorState.HSV_S, value / 255.0);
                        return Color.FromArgb((byte)(CurrentColorState.A * 255), (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
                    }
                default:
                    return Color.FromArgb((byte)(CurrentColorState.A * 255), (byte)(CurrentColorState.RGB_R * 255), (byte)(CurrentColorState.RGB_G * 255), (byte)(CurrentColorState.RGB_B * 255));
            }
        }
    }
}