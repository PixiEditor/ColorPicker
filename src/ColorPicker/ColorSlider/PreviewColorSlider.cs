using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ColorPicker.Models;
using ColorPicker.Models.ColorSliders;

namespace ColorPicker.ColorSlider
{
    internal sealed class PreviewColorSlider : Slider, INotifyPropertyChanged
    {
        public static readonly DependencyProperty CurrentColorStateProperty =
            DependencyProperty.Register(nameof(CurrentColorState), typeof(ColorState), typeof(PreviewColorSlider),
                new PropertyMetadata(ColorStateChangedCallback));

        public static readonly DependencyProperty SmallChangeBindableProperty =
            DependencyProperty.Register(nameof(SmallChangeBindable), typeof(double), typeof(PreviewColorSlider),
                new PropertyMetadata(1.0, SmallChangeBindableChangedCallback));
        
        public static readonly DependencyProperty SliderTypeProperty =
            DependencyProperty.Register(nameof(SliderTypeProperty), typeof(ColorSliderType), typeof(PreviewColorSlider),
                new PropertyMetadata(ColorSliderType.RgbRed, ColorSliderTypeChangedCallback));
        
        public ColorSliderType SliderType
        {
            get => (ColorSliderType)GetValue(SliderTypeProperty);
            set => SetValue(SliderTypeProperty, value);
        }
        
        public double SmallChangeBindable
        {
            get => (double)GetValue(SmallChangeBindableProperty);
            set => SetValue(SmallChangeBindableProperty, value);
        }

        public ColorState CurrentColorState
        {
            get => (ColorState)GetValue(CurrentColorStateProperty);
            set => SetValue(CurrentColorStateProperty, value);
        }
        
        
        public GradientStopCollection BackgroundGradient
        {
            get => backgroundBrush.GradientStops;
            set => backgroundBrush.GradientStops = value;
        }
        
        private readonly LinearGradientBrush backgroundBrush = new();
        private SolidColorBrush leftCapColor = new();
        private SolidColorBrush rightCapColor = new();

        private IColorSliderType colorSliderTypeImpl;
        
        public SolidColorBrush LeftCapColor
        {
            get => leftCapColor;
            set
            {
                leftCapColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LeftCapColor)));
            }
        }

        public SolidColorBrush RightCapColor
        {
            get => rightCapColor;
            set
            {
                rightCapColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RightCapColor)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public PreviewColorSlider()
        {
            Minimum = 0;
            Maximum = 255;
            SmallChange = 1;
            LargeChange = 10;
            MinHeight = 12;
            PreviewMouseWheel += OnPreviewMouseWheel;
        }        
        
        public override void EndInit()
        {
            base.EndInit();
            Background = backgroundBrush;
            GenerateBackground();
        }

        private void GenerateBackground()
        {
            if (colorSliderTypeImpl is null)
                return;

            List<ColorSliderGradientPoint> points = colorSliderTypeImpl.CalculateRgbGradient(CurrentColorState);


            int lastIndex = points.Count - 1;
            LeftCapColor.Color = Color.FromArgb((byte)(points[0].A * 255), (byte)(points[0].R * 255), (byte)(points[0].G * 255), (byte)(points[0].B * 255));
            RightCapColor.Color = Color.FromArgb((byte)(points[lastIndex].A * 255), (byte)(points[lastIndex].R * 255), (byte)(points[lastIndex].G * 255), (byte)(points[lastIndex].B * 255));
            
            GradientStopCollection collection = new(points.Count);
            foreach (ColorSliderGradientPoint point in points)
            {
                GradientStop stop = new(Color.FromArgb((byte)(point.A * 255), (byte)(point.R * 255), (byte)(point.G * 255), (byte)(point.B * 255)), point.Position);
                collection.Add(stop);
            }

            BackgroundGradient = collection;
        }

        private static void ColorStateChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slider = (PreviewColorSlider)d;
            if (slider.colorSliderTypeImpl?.RefreshGradient ?? false)
                slider.GenerateBackground();
        }

        private static void SmallChangeBindableChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PreviewColorSlider)d).SmallChange = (double)e.NewValue;
        }
        
        private static void ColorSliderTypeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (PreviewColorSlider)d;
            self.colorSliderTypeImpl = ColorSliderTypeFactory.Get((ColorSliderType)e.NewValue);
            self.GenerateBackground();
        }

        private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs args)
        {
            Value = MathHelper.Clamp(Value + SmallChange * args.Delta / 120, Minimum, Maximum);
            args.Handled = true;
        }
    }
}