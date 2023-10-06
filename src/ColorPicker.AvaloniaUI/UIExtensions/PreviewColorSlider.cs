using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Reactive;
using ColorPicker.Models;

namespace ColorPicker.UIExtensions;

internal abstract class PreviewColorSlider : Slider, INotifyPropertyChanged
{
    public static readonly StyledProperty<ColorState> CurrentColorStateProperty =
        AvaloniaProperty.Register<PreviewColorSlider, ColorState>(
            nameof(CurrentColorState));

    public static readonly StyledProperty<double> SmallChangeBindableProperty =
        AvaloniaProperty.Register<PreviewColorSlider, double>(
            nameof(SmallChangeBindableProperty), 1);

    private readonly LinearGradientBrush backgroundBrush = new();

    private SolidColorBrush _leftCapColor = new();

    private SolidColorBrush _rightCapColor = new();

    static PreviewColorSlider()
    {
        CurrentColorStateProperty.Changed.Subscribe(
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs<ColorState>>(ColorStateChangedCallback));
        SmallChangeBindableProperty.Changed.Subscribe(
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs<double>>(SmallChangeBindableChangedCallback));
    }

    public PreviewColorSlider()
    {
        Minimum = 0;
        Maximum = 255;
        SmallChange = 1;
        LargeChange = 10;
        MinHeight = 12;
        PointerWheelChanged += OnPreviewMouseWheel;
    }

    public double SmallChangeBindable
    {
        get => GetValue(SmallChangeProperty);
        set => SetValue(SmallChangeProperty, value);
    }

    protected virtual bool RefreshGradient => true;

    public ColorState CurrentColorState
    {
        get => GetValue(CurrentColorStateProperty);
        set => SetValue(CurrentColorStateProperty, value);
    }

    public GradientStops BackgroundGradient
    {
        get => backgroundBrush.GradientStops;
        set => backgroundBrush.GradientStops = value;
    }

    public SolidColorBrush LeftCapColor
    {
        get => _leftCapColor;
        set
        {
            _leftCapColor = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LeftCapColor)));
        }
    }

    public SolidColorBrush RightCapColor
    {
        get => _rightCapColor;
        set
        {
            _rightCapColor = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RightCapColor)));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        if (Track != null) Track.IgnoreThumbDrag = false;
    }

    public override void EndInit()
    {
        base.EndInit();
        Background = backgroundBrush;
        GenerateBackground();
    }

    protected abstract void GenerateBackground();

    protected static void ColorStateChangedCallback(AvaloniaPropertyChangedEventArgs<ColorState> e)
    {
        var slider = (PreviewColorSlider)e.Sender;
        if (slider.RefreshGradient)
            slider.GenerateBackground();
    }

    private static void SmallChangeBindableChangedCallback(AvaloniaPropertyChangedEventArgs<double> e)
    {
        ((PreviewColorSlider)e.Sender).SmallChange = e.NewValue.Value;
    }

    private void OnPreviewMouseWheel(object sender, PointerWheelEventArgs args)
    {
        Value = MathHelper.Clamp(Value + SmallChange * args.Delta.Y / 120, Minimum, Maximum);
        args.Handled = true;
    }
}