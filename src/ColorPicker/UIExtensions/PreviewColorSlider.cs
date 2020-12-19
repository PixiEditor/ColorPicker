using ColorPicker.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker.UIExtensions
{
    public abstract class PreviewColorSlider : Slider
    {
        public static readonly DependencyProperty CurrentColorProperty =
            DependencyProperty.Register("CurrentColor", typeof(Color), typeof(PreviewColorSlider),
                new PropertyMetadata(Colors.Black, ColorChangedCallback));

        public PreviewColorSlider()
        {
            Minimum = 0;
            Maximum = 255;
            SmallChange = 1;
            LargeChange = 10;
            MinHeight = 12;
        }

        public Color CurrentColor
        {
            get => (Color)GetValue(CurrentColorProperty);
            set => SetValue(CurrentColorProperty, value);
        }

        public override void EndInit()
        {
            base.EndInit();
            GenerateBackground();
        }

        protected abstract void GenerateBackground();

        protected static void ColorChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PreviewColorSlider slider = (PreviewColorSlider)d;
            slider.GenerateBackground();
        }
    }
}