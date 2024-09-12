using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ColorPicker.Models;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.UserControls
{
    internal partial class SquareSlider : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty HueProperty
            = DependencyProperty.Register(nameof(Hue), typeof(double), typeof(SquareSlider),
                new PropertyMetadata(0.0, OnHueChanged));

        public static readonly DependencyProperty HeadXProperty
            = DependencyProperty.Register(nameof(HeadX), typeof(double), typeof(SquareSlider),
                new PropertyMetadata(0.0));

        public static readonly DependencyProperty HeadYProperty
            = DependencyProperty.Register(nameof(HeadY), typeof(double), typeof(SquareSlider),
                new PropertyMetadata(0.0));

        public static readonly DependencyProperty PickerTypeProperty
            = DependencyProperty.Register(nameof(PickerType), typeof(PickerType), typeof(SquareSlider),
                new PropertyMetadata(PickerType.HSV, OnColorSpaceChanged));

        private WriteableBitmap _gradientBitmap;

        private double _rangeX;
        private double _rangeY;

        private Action recalculateGradientMethod;
        
        public SquareSlider()
        {
            GradientBitmap = new WriteableBitmap(32, 32, 96, 96, PixelFormats.Rgb24, null);
            InitializeComponent();
            recalculateGradientMethod = RecalculateGradientHsv;
            recalculateGradientMethod();
        }
        
        public double Hue
        {
            get => (double)GetValue(HueProperty);
            set => SetValue(HueProperty, value);
        }

        public double HeadX
        {
            get => (double)GetValue(HeadXProperty);
            set => SetValue(HeadXProperty, value);
        }

        public double HeadY
        {
            get => (double)GetValue(HeadYProperty);
            set => SetValue(HeadYProperty, value);
        }

        public PickerType PickerType
        {
            get => (PickerType)GetValue(PickerTypeProperty);
            set => SetValue(PickerTypeProperty, value);
        }

        public double RangeX
        {
            get => _rangeX;
            set
            {
                _rangeX = value;
                RaisePropertyChanged(nameof(RangeX));
            }
        }

        public double RangeY
        {
            get => _rangeY;
            set
            {
                _rangeY = value;
                RaisePropertyChanged(nameof(RangeY));
            }
        }

        public WriteableBitmap GradientBitmap
        {
            get => _gradientBitmap;
            set
            {
                _gradientBitmap = value;
                RaisePropertyChanged(nameof(GradientBitmap));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RecalculateGradientHsv()
        {
            var w = GradientBitmap.PixelWidth;
            var h = GradientBitmap.PixelHeight;
            var hue = Hue;
            var pixels = new byte[w * h * 3];
            for (var j = 0; j < h; j++)
            for (var i = 0; i < w; i++)
            {
                var rgb = RgbHelper.HslToRgb(hue, i / (double)(w - 1), (h - 1 - j) / (double)(h - 1));
                var pos = (j * h + i) * 3;
                pixels[pos] = (byte)(rgb.R * 255);
                pixels[pos + 1] = (byte)(rgb.G * 255);
                pixels[pos + 2] = (byte)(rgb.B * 255);
            }

            GradientBitmap.WritePixels(new Int32Rect(0, 0, w, h), pixels, w * 3, 0);
        }

        private void RecalculateGradientHsl()
        {
            var w = GradientBitmap.PixelWidth;
            var h = GradientBitmap.PixelHeight;
            var hue = Hue;
            var pixels = new byte[w * h * 3];
            for (var j = 0; j < h; j++)
            for (var i = 0; i < w; i++)
            {
                var rgb = RgbHelper.HslToRgb(hue, i / (double)(w - 1), (h - 1 - j) / (double)(h - 1));
                var pos = (j * h + i) * 3;
                pixels[pos] = (byte)(rgb.R * 255);
                pixels[pos + 1] = (byte)(rgb.G * 255);
                pixels[pos + 2] = (byte)(rgb.B * 255);
            }

            GradientBitmap.WritePixels(new Int32Rect(0, 0, w, h), pixels, w * 3, 0);
        }

        private void RecalculateGradientOkHsv()
        {
            var w = GradientBitmap.PixelWidth;
            var h = GradientBitmap.PixelHeight;
            var hue = Hue;
            var pixels = new byte[w * h * 3];
            for (var j = 0; j < h; j++)
            for (var i = 0; i < w; i++)
            {
                var rgb = RgbHelper.OkHsvToRgb(hue, i / (double)(w - 1), (h - 1 - j) / (double)(h - 1));
                var pos = (j * h + i) * 3;
                pixels[pos] = (byte)(rgb.R * 255);
                pixels[pos + 1] = (byte)(rgb.G * 255);
                pixels[pos + 2] = (byte)(rgb.B * 255);
            }

            GradientBitmap.WritePixels(new Int32Rect(0, 0, w, h), pixels, w * 3, 0);
        }

        private void RecalculateGradientOkHsl()
        {
            var w = GradientBitmap.PixelWidth;
            var h = GradientBitmap.PixelHeight;
            var hue = Hue;
            var pixels = new byte[w * h * 3];
            for (var j = 0; j < h; j++)
            for (var i = 0; i < w; i++)
            {
                var rgb = RgbHelper.OkHslToRgb(hue, i / (double)(w - 1), (h - 1 - j) / (double)(h - 1));
                var pos = (j * h + i) * 3;
                pixels[pos] = (byte)(rgb.R * 255);
                pixels[pos + 1] = (byte)(rgb.G * 255);
                pixels[pos + 2] = (byte)(rgb.B * 255);
            }

            GradientBitmap.WritePixels(new Int32Rect(0, 0, w, h), pixels, w * 3, 0);
        }

        private static void OnColorSpaceChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var sender = (SquareSlider)d;
            switch ((PickerType)args.NewValue)
            {
                case PickerType.HSV:
                    sender.recalculateGradientMethod = sender.RecalculateGradientHsv;
                    break;
                case PickerType.HSL:
                    sender.recalculateGradientMethod = sender.RecalculateGradientHsl;
                    break;
                case PickerType.OKHSV:
                    sender.recalculateGradientMethod = sender.RecalculateGradientOkHsv;
                    break;
                case PickerType.OKHSL:
                    sender.recalculateGradientMethod = sender.RecalculateGradientOkHsl;
                    break;
                default:
                    sender.recalculateGradientMethod = sender.RecalculateGradientHsl;
                    break;
            }

            sender.recalculateGradientMethod();
        }

        private static void OnHueChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((SquareSlider)d).recalculateGradientMethod();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ((UIElement)sender).CaptureMouse();
            UpdatePos(e.GetPosition(this));
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var grid = (Grid)sender;
            if (grid.IsMouseCaptured)
                UpdatePos(e.GetPosition(this));
        }

        private void UpdatePos(Point pos)
        {
            HeadX = MathHelper.Clamp(pos.X / ActualWidth, 0, 1) * RangeX;
            HeadY = (1 - MathHelper.Clamp(pos.Y / ActualHeight, 0, 1)) * RangeY;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            ((UIElement)sender).ReleaseMouseCapture();
        }

        private void RaisePropertyChanged(string property)
        {
            if (property != null) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}