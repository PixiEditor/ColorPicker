using ColorPicker.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker.UIExtensions
{
    public class RgbColorSlider : Slider
    {
        // Using a DependencyProperty as the backing store for SliderArgbType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SliderArgbTypeProperty =
            DependencyProperty.Register("SliderArgbType", typeof(string), typeof(RgbColorSlider),
                new PropertyMetadata(""));

        // Using a DependencyProperty as the backing store for CurrentColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentColorProperty =
            DependencyProperty.Register("CurrentColor", typeof(Color), typeof(RgbColorSlider),
                new PropertyMetadata(Colors.Black, ColorChangedCallback));

        public RgbColorSlider()
        {
            Minimum = 0;
            Maximum = 255;
            SmallChange = 1;
            LargeChange = 10;
            MinHeight = 12;
        }


        public string SliderArgbType
        {
            get => (string) GetValue(SliderArgbTypeProperty);
            set => SetValue(SliderArgbTypeProperty, value);
        }


        public Color CurrentColor
        {
            get => (Color) GetValue(CurrentColorProperty);
            set => SetValue(CurrentColorProperty, value);
        }

        public override void EndInit()
        {
            base.EndInit();
            GenerateBackground();
        }


        private void GenerateBackground()
        {
            if (SliderArgbType == "H")
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
            switch (SliderArgbType)
            {
                case "A":
                    return Color.FromArgb((byte)value, CurrentColor.R, CurrentColor.G, CurrentColor.B);
                case "R":
                    return Color.FromArgb(CurrentColor.A, (byte)value, CurrentColor.G, CurrentColor.B);
                case "G":
                    return Color.FromArgb(CurrentColor.A, CurrentColor.R, (byte)value, CurrentColor.B);
                case "B":
                    return Color.FromArgb(CurrentColor.A, CurrentColor.R, CurrentColor.G, (byte)value);
                case "H":
                    {
                        var (h, s, v) = HsvConverter.RgbToHsv(CurrentColor.R/255.0, CurrentColor.G / 255.0, CurrentColor.B/ 255.0);
                        var (r, g, b) = HsvConverter.HsvToRgb(value, s, v);
                        return Color.FromArgb(CurrentColor.A, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
                    }
                case "S":
                    {
                        var (h, s, v) = HsvConverter.RgbToHsv(CurrentColor.R / 255.0, CurrentColor.G / 255.0, CurrentColor.B / 255.0);
                        var (r, g, b) = HsvConverter.HsvToRgb(h, value / 255.0, v);
                        return Color.FromArgb(CurrentColor.A, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
                    }
                case "V":
                    {
                        var (h, s, v) = HsvConverter.RgbToHsv(CurrentColor.R / 255.0, CurrentColor.G / 255.0, CurrentColor.B / 255.0);
                        var (r, g, b) = HsvConverter.HsvToRgb(h, s, value / 255.0);
                        return Color.FromArgb(CurrentColor.A, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
                    }
                default:
                    return CurrentColor;
            }
        }

        private static void ColorChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RgbColorSlider slider = (RgbColorSlider) d;
            slider.GenerateBackground();
        }
    }
}