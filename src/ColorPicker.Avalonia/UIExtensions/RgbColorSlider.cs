using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;
using SharpDX.Direct2D1;
using System;

namespace ColorPicker.UIExtensions
{
    public class RgbColorSlider : Slider
    {
        // Using a DependencyProperty as the backing store for SliderArgbType.  This enables animation, styling, binding, etc...
        public static readonly StyledProperty<string> SliderArgbTypeProperty =
            AvaloniaProperty.Register<RgbColorSlider, string>("SliderArgbType");

        // Using a DependencyProperty as the backing store for CurrentColor.  This enables animation, styling, binding, etc...
        public static readonly StyledProperty<Color> CurrentColorProperty =
            AvaloniaProperty.Register<RgbColorSlider, Color>("CurrentColor");

        public RgbColorSlider()
        {
            Minimum = 0;
            Maximum = 255;
            SmallChange = 1;
            LargeChange = 10;
            MinHeight = 12;
            this.WhenAnyValue(x => x.CurrentColor).Subscribe(_=> GenerateBackground());
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
            Background = new Avalonia.Media.LinearGradientBrush()
            {
                GradientStops = new GradientStops()
                {
                    new Avalonia.Media.GradientStop(GetColorForSelectedArgb(0), 0.0),
                    new Avalonia.Media.GradientStop(GetColorForSelectedArgb(255), 1)
                },
            };
        }

        private Color GetColorForSelectedArgb(byte value)
        {
            return SliderArgbType switch
            {
                "A" => Color.FromArgb(value, CurrentColor.R, CurrentColor.G, CurrentColor.B),
                "R" => Color.FromArgb(CurrentColor.A, value, CurrentColor.G, CurrentColor.B),
                "G" => Color.FromArgb(CurrentColor.A, CurrentColor.R, value, CurrentColor.B),
                "B" => Color.FromArgb(CurrentColor.A, CurrentColor.R, CurrentColor.G, value),
                _ => CurrentColor
            };
        }
    }
}