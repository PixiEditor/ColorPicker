using System.Windows;
using System.Windows.Media;

namespace ColorPicker.UIExtensions
{
    internal class RgbColorSlider : PreviewColorSlider
    {
        public static readonly DependencyProperty SliderArgbTypeProperty =
            DependencyProperty.Register(nameof(SliderArgbType), typeof(string), typeof(RgbColorSlider),
                new PropertyMetadata(""));

        public RgbColorSlider() : base() { }

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
            byte a = (byte)(CurrentColorState.A * 255);
            byte r = (byte)(CurrentColorState.RGB_R * 255);
            byte g = (byte)(CurrentColorState.RGB_G * 255);
            byte b = (byte)(CurrentColorState.RGB_B * 255);
            return SliderArgbType switch
            {
                "A" => Color.FromArgb((byte)value, r, g, b),
                "R" => Color.FromArgb(a, (byte)value, g, b),
                "G" => Color.FromArgb(a, r, (byte)value, b),
                "B" => Color.FromArgb(a, r, g, (byte)value),
                _ => Color.FromArgb(a, r, g, b),
            };
        }
    }
}
