using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Reactive;
using ColorPicker.Models;

namespace ColorPicker;

public class PickerControlBase : TemplatedControl, IColorStateStorage
{
    public static readonly StyledProperty<ColorState> ColorStateProperty =
        AvaloniaProperty.Register<PickerControlBase, ColorState>(
            nameof(ColorState), new ColorState(0, 0, 0, 1, 0, 0, 0, 0, 0, 0));

    public static readonly StyledProperty<Color> SelectedColorProperty =
        AvaloniaProperty.Register<PickerControlBase, Color>(
            nameof(SelectedColor), Colors.Black);

    public static readonly StyledProperty<NotifyableColor> ColorProperty =
        AvaloniaProperty.Register<PickerControlBase, NotifyableColor>(
            nameof(Color));

    public static readonly RoutedEvent ColorChangedEvent =
        RoutedEvent.Register<PickerControlBase, ColorRoutedEventArgs>(
            nameof(ColorChanged), RoutingStrategies.Bubble);

    public static readonly StyledProperty<IBrush> SelectedBrushProperty =
        AvaloniaProperty.Register<DualPickerControlBase, IBrush>(
            nameof(SelectedBrush));

    private bool ignoreColorChange;

    private bool ignoreColorPropertyChange;
    protected bool ignoreSelectedBrushChange;
    private Color previousColor = Avalonia.Media.Color.FromArgb(5, 5, 5, 5);

    static PickerControlBase()
    {
        ColorStateProperty.Changed.Subscribe(
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs<ColorState>>(OnColorStatePropertyChange));
        SelectedColorProperty.Changed.Subscribe(
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs<Color>>(OnSelectedColorPropertyChange));
        SelectedBrushProperty.Changed.Subscribe(
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs<IBrush>>(OnSelectedBrushPropertyChange));
    }

    public PickerControlBase()
    {
        Color = new NotifyableColor(this);
        SelectedBrush = new SolidColorBrush(Colors.Black);
        Color.UpdateAllCompleted += (sender, args) =>
        {
            var newColor = Avalonia.Media.Color.FromArgb(
                (byte)Math.Round(Color.A),
                (byte)Math.Round(Color.RGB_R),
                (byte)Math.Round(Color.RGB_G),
                (byte)Math.Round(Color.RGB_B));
            if (newColor != previousColor)
            {
                RaiseEvent(new ColorRoutedEventArgs(ColorChangedEvent, newColor));
                previousColor = newColor;
            }
        };
        ColorChanged += (sender, newColor) =>
        {
            if (!ignoreColorChange)
            {
                ignoreColorPropertyChange = true;
                SelectedColor = ((ColorRoutedEventArgs)newColor).Color;
                ignoreColorPropertyChange = false;
            }
        };
    }

    public NotifyableColor Color
    {
        get => GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public Color SelectedColor
    {
        get => GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }

    public ColorState ColorState
    {
        get => GetValue(ColorStateProperty);
        set => SetValue(ColorStateProperty, value);
    }

    public IBrush SelectedBrush
    {
        get => GetValue(SelectedBrushProperty);
        set => SetValue(SelectedBrushProperty, value);
    }

    public event EventHandler<RoutedEventArgs> ColorChanged
    {
        add => AddHandler(ColorChangedEvent, value);
        remove => RemoveHandler(ColorChangedEvent, value);
    }

    protected virtual void UpdateSelectedBrush()
    {
        ignoreSelectedBrushChange = true;
        SelectedBrush = new SolidColorBrush(SelectedColor);
        ignoreSelectedBrushChange = false;
    }

    protected virtual void UpdateFromBrush(IBrush brush)
    {
        if (brush is ISolidColorBrush solidColorBrush)
        {
            SelectedColor = solidColorBrush.Color;
        }
    }

    private static void OnColorStatePropertyChange(AvaloniaPropertyChangedEventArgs<ColorState> args)
    {
        PickerControlBase picker = args.Sender as PickerControlBase;
        picker.Color.UpdateEverything(args.OldValue.Value);
        picker.UpdateSelectedBrush();
    }

    private static void OnSelectedColorPropertyChange(AvaloniaPropertyChangedEventArgs<Color> args)
    {
        var sender = (PickerControlBase)args.Sender;
        if (sender.ignoreColorPropertyChange)
            return;
        var newValue = args.NewValue.Value;
        sender.ignoreColorChange = true;
        sender.Color.A = newValue.A;
        sender.Color.RGB_R = newValue.R;
        sender.Color.RGB_G = newValue.G;
        sender.Color.RGB_B = newValue.B;
        sender.ignoreColorChange = false;
    }

    private static void OnSelectedBrushPropertyChange(AvaloniaPropertyChangedEventArgs<IBrush> args)
    {
        var sender = (PickerControlBase)args.Sender;
        if (sender.ignoreSelectedBrushChange)
            return;

        sender.ignoreColorChange = true;
        sender.UpdateFromBrush(args.NewValue.Value);
        sender.ignoreColorChange = false;
    }
}
