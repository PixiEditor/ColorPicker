using ColorPicker.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ColorPicker.UIExtensions
{
    internal abstract class PreviewColorSlider : Slider
    {
        public static readonly DependencyProperty CurrentColorProperty =
            DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(PreviewColorSlider),
                new PropertyMetadata(Colors.Black, ColorChangedCallback));

        public static readonly DependencyProperty SmallChangeBindableProperty =
            DependencyProperty.Register(nameof(SmallChangeBindable), typeof(double), typeof(PreviewColorSlider),
                new PropertyMetadata(1.0, SmallChangeBindableChangedCallback));

        public PreviewColorSlider()
        {
            Minimum = 0;
            Maximum = 255;
            SmallChange = 1;
            LargeChange = 10;
            MinHeight = 12;
            PreviewMouseWheel += OnPreviewMouseWheel;
        }

        public double SmallChangeBindable
        {
            get => (double)GetValue(SmallChangeBindableProperty);
            set => SetValue(SmallChangeBindableProperty, value);
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

        private static void SmallChangeBindableChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PreviewColorSlider)d).SmallChange = (double)e.NewValue;
        }

        private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs args)
        {
            Value = Math.Clamp(Value + SmallChange * args.Delta / 120, Minimum, Maximum);
            args.Handled = true;
        }
    }
}