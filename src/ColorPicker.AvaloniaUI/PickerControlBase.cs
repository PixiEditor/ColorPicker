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

    private bool ignoreColorChange;

    private bool ignoreColorPropertyChange;
    private Color previousColor = Avalonia.Media.Color.FromArgb(5, 5, 5, 5);

    static PickerControlBase()
    {
        ColorStateProperty.Changed.Subscribe(
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs<ColorState>>(OnColorStatePropertyChange));
        SelectedColorProperty.Changed.Subscribe(
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs<Color>>(OnSelectedColorPropertyChange));

        IsEffectivelyEnabledProperty.Changed.Subscribe(new AnonymousObserver<AvaloniaPropertyChangedEventArgs<bool>>(e =>
        {
            if (e.Sender is PickerControlBase sender)
            {
                var color = Avalonia.Media.Color.FromArgb(
                (byte)Math.Round(sender.Color.A),
                (byte)Math.Round(sender.Color.RGB_R),
                (byte)Math.Round(sender.Color.RGB_G),
                (byte)Math.Round(sender.Color.RGB_B));
                sender.updateColorAction(sender, new ColorRoutedEventArgs(ColorChangedEvent, color));
            }
        }));
    }

    public PickerControlBase()
    {
        Color = new NotifyableColor(this);
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

        

        ColorChanged += (sender, args) => updateColorAction(sender, args);
        
    }

    private Action<object, RoutedEventArgs>  updateColorAction => new Action<object, RoutedEventArgs>
    ((sender, newColor) =>
    {
        if (!ignoreColorChange)
        {
            ignoreColorPropertyChange = true;
            if (IsEffectivelyEnabled)
                SelectedColor = ((ColorRoutedEventArgs)newColor).Color;
            else
            {
                var grayColor = ((ColorRoutedEventArgs)newColor).Color.R * 0.21
                                + ((ColorRoutedEventArgs)newColor).Color.G * 0.72
                                + ((ColorRoutedEventArgs)newColor).Color.B * 0.07;
                SelectedColor = Avalonia.Media.Color.FromArgb(
                    ((ColorRoutedEventArgs)newColor).Color.A,
                    (byte)grayColor,
                    (byte)grayColor,
                    (byte)grayColor);
            }
            ignoreColorPropertyChange = false;
        }
    });

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

    public event EventHandler<RoutedEventArgs> ColorChanged
    {
        add => AddHandler(ColorChangedEvent, value);
        remove => RemoveHandler(ColorChangedEvent, value);
    }

    private static void OnColorStatePropertyChange(AvaloniaPropertyChangedEventArgs<ColorState> args)
    {
        ((PickerControlBase)args.Sender).Color.UpdateEverything(args.OldValue.Value);
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
}