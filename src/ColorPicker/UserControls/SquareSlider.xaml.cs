using ColorPicker.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorPicker.UserControls
{
    internal partial class SquareSlider : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty HueProperty = DependencyProperty.Register(nameof(Hue), typeof(double), typeof(SquareSlider), new PropertyMetadata(0.0, OnHueChanged));
        public static readonly DependencyProperty HeadXProperty = DependencyProperty.Register(nameof(HeadX), typeof(double), typeof(SquareSlider), new PropertyMetadata(0.0));
        public static readonly DependencyProperty HeadYProperty = DependencyProperty.Register(nameof(HeadY), typeof(double), typeof(SquareSlider), new PropertyMetadata(0.0));

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
        private double _rangeX;
        public double RangeX
        {
            get => _rangeX;
            set
            {
                _rangeX = value;
                RaisePropertyChanged(nameof(RangeX));
            }
        }
        private double _rangeY;
        public double RangeY
        {
            get => _rangeY;
            set
            {
                _rangeY = value;
                RaisePropertyChanged(nameof(RangeY));
            }
        }
        public SquareSlider()
        {
            GradientBitmap = new WriteableBitmap(32, 32, 96, 96, PixelFormats.Rgb24, null);
            InitializeComponent();
            RecalculateGradient();
        }

        private WriteableBitmap _gradientBitmap;

        public WriteableBitmap GradientBitmap
        {
            get => _gradientBitmap;
            set
            {
                _gradientBitmap = value;
                RaisePropertyChanged(nameof(GradientBitmap));
            }
        }

        private void RecalculateGradient()
        {
            int w = GradientBitmap.PixelWidth;
            int h = GradientBitmap.PixelHeight;
            double hue = Hue;
            byte[] pixels = new byte[w * h * 3];
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    var rgbtuple = HsvHelper.HsvToRgb(hue, i / (double)(w - 1), ((h - 1) - j) / (double)(h - 1));
                    double r = rgbtuple.Item1, g = rgbtuple.Item2, b = rgbtuple.Item3;
                    int pos = (j * h + i) * 3;
                    pixels[pos] = (byte)(r * 255);
                    pixels[pos + 1] = (byte)(g * 255);
                    pixels[pos + 2] = (byte)(b * 255);
                }
            }
            GradientBitmap.WritePixels(new Int32Rect(0, 0, w, h), pixels, w * 3, 0);
        }
        private static void OnHueChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((SquareSlider)d).RecalculateGradient();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ((UIElement)sender).CaptureMouse();
            UpdatePos(e.GetPosition(this));
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            Grid grid = (Grid)sender;
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
