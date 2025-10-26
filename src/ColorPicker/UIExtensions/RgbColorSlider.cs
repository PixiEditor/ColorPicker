using System.Windows;
using System.Windows.Media;

namespace ColorPicker.UIExtensions
{
    internal class RgbColorSlider : PreviewColorSlider
    {
        public static readonly DependencyProperty SliderArgbTypeProperty =
            DependencyProperty.Register(nameof(SliderArgbType), typeof(string), typeof(RgbColorSlider),
                new PropertyMetadata(""));

        public string SliderArgbType
        {
            get => (string)GetValue(SliderArgbTypeProperty);
            set => SetValue(SliderArgbTypeProperty, value);
        }

        protected override void GenerateBackground()
        {
            var colorStart = GetColorForSelectedArgb(0);
            var colorEnd = GetColorForSelectedArgb(255);
            LeftCapColor.Color = colorStart;
            RightCapColor.Color = colorEnd;
            BackgroundGradient = new GradientStopCollection
            {
                new GradientStop(colorStart, 0.0),
                new GradientStop(colorEnd, 1)
            };
        }

        private Color GetColorForSelectedArgb(int value)
        {
            var a = (byte)(CurrentColorState.A * 255);
            var r = (byte)(CurrentColorState.RGB_R * 255);
            var g = (byte)(CurrentColorState.RGB_G * 255);
            var b = (byte)(CurrentColorState.RGB_B * 255);
            var gray = (byte)(0.21 * r + 0.72 * g + 0.07 * b);
            switch (SliderArgbType)
            {
                case "A": return IsEnabled ? Color.FromArgb((byte)value, r, g, b) : Color.FromArgb((byte)value, gray, gray, gray);
                case "R": return IsEnabled ? Color.FromArgb(255, (byte)value, g, b) : Color.FromArgb(255, (byte)value, (byte)value, (byte)value);
                case "G": return IsEnabled ? Color.FromArgb(255, r, (byte)value, b) : Color.FromArgb(255, (byte)value, (byte)value, (byte)value);
                case "B": return IsEnabled ? Color.FromArgb(255, r, g, (byte)value) : Color.FromArgb(255, (byte)value, (byte)value, (byte)value);
                default: return IsEnabled ? Color.FromArgb(a, r, g, b) : Color.FromArgb(a, gray, gray, gray);
            }

            ;
        }
    }
}