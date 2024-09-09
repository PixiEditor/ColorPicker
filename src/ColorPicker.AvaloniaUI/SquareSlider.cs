using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Data.Core;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Reactive;
using ColorPicker.AvaloniaUI;
using ColorPicker.Models;

namespace ColorPicker.UserControls;

[PseudoClasses(":hsv", ":hsl")]
[TemplatePart(Name = "PART_GradientImage", Type = typeof(Image))]
internal class SquareSlider : TemplatedControl
{
    public static readonly StyledProperty<double> HueProperty = AvaloniaProperty.Register<SquareSlider, double>(
        nameof(Hue));

    public static readonly StyledProperty<PickerType> PickerTypeProperty =
        AvaloniaProperty.Register<SquareSlider, PickerType>(
            nameof(PickerType));

    public static readonly StyledProperty<double> HeadXProperty = AvaloniaProperty.Register<SquareSlider, double>(
        nameof(HeadX));

    public static readonly StyledProperty<double> HeadYProperty = AvaloniaProperty.Register<SquareSlider, double>(
        nameof(HeadY));

    public static readonly StyledProperty<WriteableBitmap> GradientBitmapProperty =
        AvaloniaProperty.Register<SquareSlider, WriteableBitmap>(
            nameof(GradientBitmap));

    public static readonly StyledProperty<double> RangeXProperty = AvaloniaProperty.Register<SquareSlider, double>(
        nameof(RangeX));

    public static readonly StyledProperty<double> RangeYProperty = AvaloniaProperty.Register<SquareSlider, double>(
        nameof(RangeY));

    public static readonly StyledProperty<NotifyableColor> ColorProperty = AvaloniaProperty.Register<SquareSlider, NotifyableColor>(
        nameof(Color));

    public NotifyableColor Color
    {
        get => GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    private Func<double, double, double, Tuple<double, double, double>> colorSpaceConversionMethod =
        ColorSpaceHelper.HsvToRgb;

    private IDisposable headXBinding;
    private IDisposable headYBinding;
    private Image image;

    static SquareSlider()
    {
        HueProperty.Changed.Subscribe(new AnonymousObserver<AvaloniaPropertyChangedEventArgs<double>>(OnHueChanged));
        PickerTypeProperty.Changed.Subscribe(
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs<PickerType>>(OnColorSpaceChanged));
    }

    public double RangeY
    {
        get => GetValue(RangeYProperty);
        set => SetValue(RangeYProperty, value);
    }

    public double RangeX
    {
        get => GetValue(RangeXProperty);
        set => SetValue(RangeXProperty, value);
    }

    public double Hue
    {
        get => GetValue(HueProperty);
        set => SetValue(HueProperty, value);
    }

    public double HeadX
    {
        get => GetValue(HeadXProperty);
        set => SetValue(HeadXProperty, value);
    }

    public double HeadY
    {
        get => GetValue(HeadYProperty);
        set => SetValue(HeadYProperty, value);
    }

    public PickerType PickerType
    {
        get => GetValue(PickerTypeProperty);
        set => SetValue(PickerTypeProperty, value);
    }


    public WriteableBitmap GradientBitmap
    {
        get => GetValue(GradientBitmapProperty);
        set => SetValue(GradientBitmapProperty, value);
    }

    public SquareSlider()
    {
        GradientBitmap = new WriteableBitmap(new PixelSize(32, 32), new Vector(96, 96), PixelFormats.Rgb24);
        PseudoClasses.Set(":hsv", true);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        image = e.NameScope.Find<Image>("PART_GradientImage");

        UpdateHeadBindings(this, PickerType);
        RecalculateGradient();
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        e.Pointer.Capture(this);
        UpdatePos(e.GetPosition(this));
        
        e.Handled = true;
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
        e.Pointer.Capture(null);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);
        if (Equals(e.Pointer.Captured, this))
        {
            UpdatePos(e.GetPosition(this));
            
            e.Handled = true;
        }
    }

    private void RecalculateGradient()
    {
        var w = GradientBitmap.PixelSize.Width;
        var h = GradientBitmap.PixelSize.Height;
        var hue = Hue;
        var pixels = new byte[w * h * 3];
        for (var j = 0; j < h; j++)
        for (var i = 0; i < w; i++)
        {
            var rgbtuple = colorSpaceConversionMethod(hue, i / (double)(w - 1), (h - 1 - j) / (double)(h - 1));
            double r = rgbtuple.Item1, g = rgbtuple.Item2, b = rgbtuple.Item3;
            var pos = (j * h + i) * 3;
            pixels[pos] = (byte)(r * 255);
            pixels[pos + 1] = (byte)(g * 255);
            pixels[pos + 2] = (byte)(b * 255);
        }

        using (var framebuffer = GradientBitmap.Lock())
        {
            framebuffer.WritePixels(0, 0, w, h, pixels);
        }

        image?.InvalidateVisual();
    }

    private static void OnColorSpaceChanged(AvaloniaPropertyChangedEventArgs<PickerType> args)
    {
        var sender = (SquareSlider)args.Sender;
        if (args.NewValue.Value == PickerType.HSV)
            sender.colorSpaceConversionMethod = ColorSpaceHelper.HsvToRgb;
        else
            sender.colorSpaceConversionMethod = ColorSpaceHelper.HslToRgb;

        sender.RecalculateGradient();
        sender.PseudoClasses.Set(":hsv", args.NewValue.Value == PickerType.HSV);
        sender.PseudoClasses.Set(":hsl", args.NewValue.Value == PickerType.HSL);
        UpdateHeadBindings(sender, args.NewValue.Value);
    }

    private static void UpdateHeadBindings(SquareSlider squareSlider, PickerType pickerType)
    {
        string headXPath = pickerType == PickerType.HSV ? "HSV_S" : "HSL_S";
        Binding headXBinding = new()
        {
            Path = headXPath,
            Mode = BindingMode.TwoWay,
            Source = squareSlider.Color
        };

        string headYPath = pickerType == PickerType.HSV ? "HSV_V" : "HSL_L";
        Binding headYBinding = new()
        {
            Path = headYPath,
            Mode = BindingMode.TwoWay,
            Source = squareSlider.Color
        };

        squareSlider.headXBinding?.Dispose();
        squareSlider.headYBinding?.Dispose();

        squareSlider.headXBinding = squareSlider.Bind(HeadXProperty, headXBinding);
        squareSlider.headYBinding = squareSlider.Bind(HeadYProperty, headYBinding);
    }

    private static void OnHueChanged(AvaloniaPropertyChangedEventArgs<double> args)
    {
        ((SquareSlider)args.Sender).RecalculateGradient();
    }

    private void UpdatePos(Point pos)
    {
        HeadX = MathHelper.Clamp(pos.X / Bounds.Width, 0, 1) * RangeX;
        HeadY = (1 - MathHelper.Clamp(pos.Y / Bounds.Height, 0, 1)) * RangeY;
    }
}