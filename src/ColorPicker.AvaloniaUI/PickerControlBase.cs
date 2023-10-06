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
            nameof(BindableColor));

    public static readonly RoutedEvent ColorChangedEvent =
        RoutedEvent.Register<PickerControlBase, ColorRoutedEventArgs>(
            nameof(ColorChanged), RoutingStrategies.Bubble);

    private bool ignoreColorChange;

    private bool ignoreColorPropertyChange;
    private Color previousColor = Color.FromArgb(5, 5, 5, 5);

    static PickerControlBase()
    {
        ColorStateProperty.Changed.Subscribe(
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs<ColorState>>(OnColorStatePropertyChange));
        SelectedColorProperty.Changed.Subscribe(
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs<Color>>(OnSelectedColorPropertyChange));
    }

    public PickerControlBase()
    {
        BindableColor = new NotifyableColor(this);
        BindableColor.PropertyChanged += (sender, args) =>
        {
            var newColor = Color.FromArgb(
                (byte)Math.Round(BindableColor.A),
                (byte)Math.Round(BindableColor.RGB_R),
                (byte)Math.Round(BindableColor.RGB_G),
                (byte)Math.Round(BindableColor.RGB_B));
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

    public NotifyableColor BindableColor
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

    public event EventHandler<RoutedEventArgs> ColorChanged
    {
        add => AddHandler(ColorChangedEvent, value);
        remove => RemoveHandler(ColorChangedEvent, value);
    }

    private static void OnColorStatePropertyChange(AvaloniaPropertyChangedEventArgs<ColorState> args)
    {
        ((PickerControlBase)args.Sender).BindableColor.UpdateEverything(args.OldValue.Value);
    }

    private static void OnSelectedColorPropertyChange(AvaloniaPropertyChangedEventArgs<Color> args)
    {
        var sender = (PickerControlBase)args.Sender;
        if (sender.ignoreColorPropertyChange)
            return;
        var newValue = args.NewValue.Value;
        sender.ignoreColorChange = true;
        sender.BindableColor.A = newValue.A;
        sender.BindableColor.RGB_R = newValue.R;
        sender.BindableColor.RGB_G = newValue.G;
        sender.BindableColor.RGB_B = newValue.B;
        sender.ignoreColorChange = false;
    }
}