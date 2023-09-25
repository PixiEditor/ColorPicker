using ColorPicker.Models;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Reactive;
using ColorPicker.AvaloniaUI;

namespace ColorPicker.UserControls
{
    [PseudoClasses(":hsv", ":hsl")]
    internal partial class SquareSlider : UserControl, INotifyPropertyChanged
    {
        public static readonly StyledProperty<double> HueProperty = AvaloniaProperty.Register<SquareSlider, double>(
            nameof(Hue), 0);

        public static readonly StyledProperty<PickerType> PickerTypeProperty = AvaloniaProperty.Register<SquareSlider, PickerType>(
            nameof(PickerType), PickerType.HSV);

        public static readonly StyledProperty<double> HeadXProperty = AvaloniaProperty.Register<SquareSlider, double>(
            nameof(HeadX), 0);

        public static readonly StyledProperty<double> HeadYProperty = AvaloniaProperty.Register<SquareSlider, double>(
            nameof(HeadY), 0);

        public double Hue
        {
            get => (double)GetValue(HueProperty);
            set => SetValue(HueProperty, value);
        }

        public double HeadX
        {
            get => GetValue(HeadXProperty);
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

        static SquareSlider()
        {
            HueProperty.Changed.Subscribe(new AnonymousObserver<AvaloniaPropertyChangedEventArgs<double>>(OnHueChanged));
            PickerTypeProperty.Changed.Subscribe(new AnonymousObserver<AvaloniaPropertyChangedEventArgs<PickerType>>(OnColorSpaceChanged));
        }

        public SquareSlider()
        {
            GradientBitmap = new WriteableBitmap(new PixelSize(32, 32), new Vector(96, 96), PixelFormats.Rgb24, null);
            PseudoClasses.Set(":hsv", true);
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

        private Func<double, double, double, Tuple<double, double, double>> colorSpaceConversionMethod = ColorSpaceHelper.HsvToRgb;

        private void RecalculateGradient()
        {
            int w = GradientBitmap.PixelSize.Width;
            int h = GradientBitmap.PixelSize.Height;
            double hue = Hue;
            byte[] pixels = new byte[w * h * 3];
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    var rgbtuple = colorSpaceConversionMethod(hue, i / (double)(w - 1), ((h - 1) - j) / (double)(h - 1));
                    double r = rgbtuple.Item1, g = rgbtuple.Item2, b = rgbtuple.Item3;
                    int pos = (j * h + i) * 3;
                    pixels[pos] = (byte)(r * 255);
                    pixels[pos + 1] = (byte)(g * 255);
                    pixels[pos + 2] = (byte)(b * 255);
                }
            }

            using var framebuffer = GradientBitmap.Lock();
            framebuffer.WritePixels(0, 0, w, h, pixels);
        }

        private static void OnColorSpaceChanged(AvaloniaPropertyChangedEventArgs<PickerType> args)
        {
            SquareSlider sender = (SquareSlider)args.Sender;
            if (args.NewValue.Value == PickerType.HSV)
                sender.colorSpaceConversionMethod = ColorSpaceHelper.HsvToRgb;
            else
                sender.colorSpaceConversionMethod = ColorSpaceHelper.HslToRgb;

            sender.RecalculateGradient();
            sender.PseudoClasses.Set(":hsv", args.NewValue.Value == PickerType.HSV);
            sender.PseudoClasses.Set(":hsl", args.NewValue.Value == PickerType.HSL);
        }
        private static void OnHueChanged(AvaloniaPropertyChangedEventArgs<double> args)
        {
            ((SquareSlider)args.Sender).RecalculateGradient();
        }

        private void OnMouseDown(object sender, PointerPressedEventArgs e)
        {
            e.Pointer.Capture((Grid)sender);
            UpdatePos(e.GetPosition(this));
        }

        private void OnMouseMove(object sender, PointerEventArgs e)
        {
            Grid grid = (Grid)sender;
            if (Equals(e.Pointer.Captured, grid))
                UpdatePos(e.GetPosition(this));
        }

        private void UpdatePos(Point pos)
        {
            HeadX = MathHelper.Clamp(pos.X / Bounds.Width, 0, 1) * RangeX;
            HeadY = (1 - MathHelper.Clamp(pos.Y / Bounds.Height, 0, 1)) * RangeY;
        }

        private void OnMouseUp(object sender, PointerReleasedEventArgs e)
        {
            e.Pointer.Capture(null);
        }

        private void RaisePropertyChanged(string property)
        {
            if (property != null) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
