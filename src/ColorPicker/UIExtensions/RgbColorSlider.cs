using ColorPicker.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker.UIExtensions
{
    public class RgbColorSlider : PreviewColorSlider
    {
        public static readonly DependencyProperty SliderArgbTypeProperty =
            DependencyProperty.Register("SliderArgbType", typeof(string), typeof(RgbColorSlider),
                new PropertyMetadata(""));

        public RgbColorSlider() : base() {}

        public string SliderArgbType
        {
            get => (string) GetValue(SliderArgbTypeProperty);
            set => SetValue(SliderArgbTypeProperty, value);
        }
        protected override void GenerateBackground()
        {
            Background = new LinearGradientBrush(new GradientStopCollection
            {
                new GradientStop(GetColorForSelectedArgb(0), 0.0),
                new GradientStop(GetColorForSelectedArgb(255), 1)
            });
        }

        private Color GetColorForSelectedArgb(int value)
        {
            return SliderArgbType switch
            {
                "A" => Color.FromArgb((byte)value, CurrentColor.R, CurrentColor.G, CurrentColor.B),
                "R" => Color.FromArgb(CurrentColor.A, (byte)value, CurrentColor.G, CurrentColor.B),
                "G" => Color.FromArgb(CurrentColor.A, CurrentColor.R, (byte)value, CurrentColor.B),
                "B" => Color.FromArgb(CurrentColor.A, CurrentColor.R, CurrentColor.G, (byte)value),
                _ => CurrentColor,
            };
        }
    }
}