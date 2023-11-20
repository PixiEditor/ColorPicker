using Avalonia;
using Avalonia.Media;
using Avalonia.Reactive;
using ColorPicker.Models;

namespace ColorPicker;

public class DualPickerControlBase : PickerControlBase, ISecondColorStorage, IHintColorStateStorage
{
    public static readonly StyledProperty<ColorState> SecondColorStateProperty =
        AvaloniaProperty.Register<DualPickerControlBase, ColorState>(
            nameof(SecondColorState), new ColorState(1, 1, 1, 1, 0, 0, 1, 0, 0, 1));

    public static readonly StyledProperty<ColorState> HintColorStateProperty =
        AvaloniaProperty.Register<DualPickerControlBase, ColorState>(
            nameof(HintColorState), new ColorState(0, 0, 0, 0, 0, 0, 0, 0, 0, 0));

    public static readonly StyledProperty<Color> SecondaryColorProperty =
        AvaloniaProperty.Register<DualPickerControlBase, Color>(
            nameof(SecondaryColor), Colors.White);

    public static readonly StyledProperty<Color> HintColorProperty =
        AvaloniaProperty.Register<DualPickerControlBase, Color>(
            nameof(HintColor), Colors.Transparent);

    public static readonly StyledProperty<bool> UseHintColorProperty =
        AvaloniaProperty.Register<DualPickerControlBase, bool>(
            nameof(UseHintColor));

    private readonly HintColorDecorator hintColorDecorator;

    private readonly SecondColorDecorator secondColorDecorator;
    private bool ignoreHintColorPropertyChange;

    private bool ignoreHintNotifyableColorChange;

    private bool ignoreSecondaryColorChange;
    private bool ignoreSecondaryColorPropertyChange;

    static DualPickerControlBase()
    {
        SecondColorStateProperty.Changed.Subscribe(
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs<ColorState>>(OnSecondColorStatePropertyChange));
        SecondaryColorProperty.Changed.Subscribe(
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs<Color>>(OnSecondaryColorPropertyChange));
        HintColorStateProperty.Changed.Subscribe(
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs<ColorState>>(OnHintColorStatePropertyChange));
        HintColorProperty.Changed.Subscribe(
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs<Color>>(OnHintColorPropertyChanged));
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        ignoreHintColorPropertyChange = false;
        ignoreSecondaryColorPropertyChange = false;
        ignoreHintNotifyableColorChange = false;
        ignoreSecondaryColorChange = false;
    }

    public DualPickerControlBase()
    {
        secondColorDecorator = new SecondColorDecorator(this);
        hintColorDecorator = new HintColorDecorator(this);

        SecondColor = new NotifyableColor(secondColorDecorator);
        SecondColor.PropertyChanged += (sender, args) =>
        {
            if (!ignoreSecondaryColorChange)
            {
                ignoreSecondaryColorPropertyChange = true;
                SecondaryColor = Avalonia.Media.Color.FromArgb(
                    (byte)Math.Round(SecondColor.A),
                    (byte)Math.Round(SecondColor.RGB_R),
                    (byte)Math.Round(SecondColor.RGB_G),
                    (byte)Math.Round(SecondColor.RGB_B));
                ignoreSecondaryColorPropertyChange = false;
            }
        };

        HintNotifyableColor = new NotifyableColor(hintColorDecorator);
        HintNotifyableColor.PropertyChanged += (sender, args) =>
        {
            if (!ignoreHintNotifyableColorChange)
            {
                ignoreHintColorPropertyChange = true;
                HintColor = Avalonia.Media.Color.FromArgb(
                    (byte)Math.Round(HintNotifyableColor.A),
                    (byte)Math.Round(HintNotifyableColor.RGB_R),
                    (byte)Math.Round(HintNotifyableColor.RGB_G),
                    (byte)Math.Round(HintNotifyableColor.RGB_B));
                ignoreHintColorPropertyChange = false;
            }
        };
    }

    public bool UseHintColor
    {
        get => GetValue(UseHintColorProperty);
        set => SetValue(UseHintColorProperty, value);
    }

    public Color HintColor
    {
        get => GetValue(HintColorProperty);
        set => SetValue(HintColorProperty, value);
    }

    public Color SecondaryColor
    {
        get => GetValue(SecondaryColorProperty);
        set => SetValue(SecondaryColorProperty, value);
    }

    public NotifyableColor SecondColor { get; set; }

    public NotifyableColor HintNotifyableColor { get; set; }

    public ColorState HintColorState
    {
        get => GetValue(HintColorStateProperty);
        set => SetValue(HintColorStateProperty, value);
    }

    public ColorState SecondColorState
    {
        get => GetValue(SecondColorStateProperty);
        set => SetValue(SecondColorStateProperty, value);
    }

    public void SwapColors()
    {
        var temp = ColorState;
        ColorState = SecondColorState;
        SecondColorState = temp;
    }

    public void SetMainColorFromHintColor()
    {
        ColorState = HintColorState;
    }

    private static void OnSecondColorStatePropertyChange(AvaloniaPropertyChangedEventArgs<ColorState> args)
    {
        ((DualPickerControlBase)args.Sender).SecondColor.UpdateEverything(args.OldValue.Value);
    }

    private static void OnHintColorStatePropertyChange(AvaloniaPropertyChangedEventArgs<ColorState> args)
    {
        ((DualPickerControlBase)args.Sender).HintNotifyableColor.UpdateEverything(args.OldValue.Value);
    }

    private static void OnHintColorPropertyChanged(AvaloniaPropertyChangedEventArgs<Color> args)
    {
        var sender = (DualPickerControlBase)args.Sender;
        var newValue = args.NewValue.Value;
        if (sender.ignoreHintColorPropertyChange)
            return;
        sender.ignoreHintNotifyableColorChange = true;
        sender.HintNotifyableColor.A = newValue.A;
        sender.HintNotifyableColor.RGB_R = newValue.R;
        sender.HintNotifyableColor.RGB_G = newValue.G;
        sender.HintNotifyableColor.RGB_B = newValue.B;
        sender.ignoreHintNotifyableColorChange = false;
    }

    private static void OnSecondaryColorPropertyChange(AvaloniaPropertyChangedEventArgs<Color> args)
    {
        var sender = (DualPickerControlBase)args.Sender;
        if (sender.ignoreSecondaryColorPropertyChange)
            return;
        var newValue = args.NewValue.Value;
        sender.ignoreSecondaryColorChange = true;
        sender.SecondColor.A = newValue.A;
        sender.SecondColor.RGB_R = newValue.R;
        sender.SecondColor.RGB_G = newValue.G;
        sender.SecondColor.RGB_B = newValue.B;
        sender.ignoreSecondaryColorChange = false;
    }
}