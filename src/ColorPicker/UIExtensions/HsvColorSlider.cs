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

        public static readonly DependencyProperty CurrentHueProperty =
            DependencyProperty.Register(nameof(CurrentHue), typeof(double), typeof(HsvColorSlider),
                new PropertyMetadata(0.0, ColorChangedCallback));

        public static readonly DependencyProperty CurrentSaturationProperty =
            DependencyProperty.Register(nameof(CurrentSaturation), typeof(double), typeof(HsvColorSlider),
                new PropertyMetadata(0.0, ColorChangedCallback));

        public HsvColorSlider() : base() { }

        public string SliderHsvType
        {
            get => (string)GetValue(SliderHsvTypeProperty);
            set => SetValue(SliderHsvTypeProperty, value);
        }

        public double CurrentHue
        {
            get => (double)GetValue(CurrentHueProperty);
            set => SetValue(CurrentHueProperty, value);
        }

        public double CurrentSaturation
        {
            get => (double)GetValue(CurrentSaturationProperty);
            set => SetValue(CurrentSaturationProperty, value);
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
            var (h, s, v) = HsvHelper.RgbToHsv(CurrentColor.R / 255.0, CurrentColor.G / 255.0, CurrentColor.B / 255.0);
            switch (SliderHsvType)
            {
                case "H":
                    {
                        var (r, g, b) = HsvHelper.HsvToRgb(value, CurrentSaturation, v);
                        return Color.FromArgb(CurrentColor.A, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
                    }
                case "S":
                    {
                        var (r, g, b) = HsvHelper.HsvToRgb(CurrentHue, value / 255.0, v);
                        return Color.FromArgb(CurrentColor.A, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
                    }
                case "V":
                    {
                        var (r, g, b) = HsvHelper.HsvToRgb(CurrentHue, CurrentSaturation, value / 255.0);
                        return Color.FromArgb(CurrentColor.A, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
                    }
                default:
                    return CurrentColor;
            }
        }
    }
}