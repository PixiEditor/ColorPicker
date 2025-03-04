using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;
using ColorPicker.Models;
using GradientStop = ColorPicker.Models.GradientStop;

namespace ColorPicker;

[TemplatePart("PART_TabControl", typeof(TabControl))]
public class StandardColorPicker : DualPickerControlBase
{
    public static readonly StyledProperty<double> SmallChangeProperty =
        AvaloniaProperty.Register<StandardColorPicker, double>(
            nameof(SmallChange),
            1.0);

    public static readonly StyledProperty<bool> ShowAlphaProperty =
        AvaloniaProperty.Register<StandardColorPicker, bool>(
            nameof(ShowAlpha),
            true);

    public static readonly StyledProperty<PickerType> PickerTypeProperty =
        AvaloniaProperty.Register<StandardColorPicker, PickerType>(
            nameof(PickerType),
            PickerType.HSV);

    public static readonly StyledProperty<bool> ShowFractionalPartProperty =
        AvaloniaProperty.Register<StandardColorPicker, bool>(
            nameof(ShowFractionalPart),
            true);

    public static readonly StyledProperty<HexRepresentationType> HexRepresentationProperty =
        AvaloniaProperty.Register<StandardColorPicker, HexRepresentationType>(
            nameof(HexRepresentation), HexRepresentationType.RGBA);

    public static readonly StyledProperty<bool> EnableGradientsTabProperty =
        AvaloniaProperty.Register<StandardColorPicker, bool>(
            nameof(EnableGradientsTab), true);

    public static readonly StyledProperty<GradientState> GradientStateProperty =
        AvaloniaProperty.Register<StandardColorPicker, GradientState>(
            nameof(GradientState));

    public static readonly StyledProperty<GradientBrush> GradientBrushProperty =
        AvaloniaProperty.Register<StandardColorPicker, GradientBrush>(
            nameof(GradientBrush));

    public static readonly StyledProperty<int> SelectedTabIndexProperty =
        AvaloniaProperty.Register<StandardColorPicker, int>(
            nameof(SelectedTabIndex));

    public static readonly StyledProperty<GradientType> GradientTypeProperty = AvaloniaProperty.Register<StandardColorPicker, GradientType>(
        nameof(GradientType));

    public GradientType GradientType
    {
        get => GetValue(GradientTypeProperty);
        set => SetValue(GradientTypeProperty, value);
    }

    public int SelectedTabIndex
    {
        get => GetValue(SelectedTabIndexProperty);
        set => SetValue(SelectedTabIndexProperty, value);
    }

    public GradientBrush GradientBrush
    {
        get => GetValue(GradientBrushProperty);
        set => SetValue(GradientBrushProperty, value);
    }

    public GradientState GradientState
    {
        get => GetValue(GradientStateProperty);
        set => SetValue(GradientStateProperty, value);
    }

    public bool EnableGradientsTab
    {
        get => GetValue(EnableGradientsTabProperty);
        set => SetValue(EnableGradientsTabProperty, value);
    }

    public HexRepresentationType HexRepresentation
    {
        get => GetValue(HexRepresentationProperty);
        set => SetValue(HexRepresentationProperty, value);
    }

    public double SmallChange
    {
        get => GetValue(SmallChangeProperty);
        set => SetValue(SmallChangeProperty, value);
    }

    public bool ShowAlpha
    {
        get => GetValue(ShowAlphaProperty);
        set => SetValue(ShowAlphaProperty, value);
    }

    public PickerType PickerType
    {
        get => GetValue(PickerTypeProperty);
        set => SetValue(PickerTypeProperty, value);
    }

    public bool ShowFractionalPart
    {
        get => GetValue(ShowFractionalPartProperty);
        set => SetValue(ShowFractionalPartProperty, value);
    }

    private TabControl tabControl;

    static StandardColorPicker()
    {
        GradientBrushProperty.Changed.Subscribe(OnGradientChanged);
        SelectedTabIndexProperty.Changed.Subscribe(SelectedTabChanged);
        GradientTypeProperty.Changed.Subscribe(GradientTypeChanged);
    }

    public StandardColorPicker()
    {
        ColorState stop0 = new();
        stop0.SetARGB(1, 0, 0, 0);
        ColorState stop1 = new();
        stop1.SetARGB(1, 1, 1, 1);

        GradientState = new GradientState(new List<GradientStop>
        {
            new GradientStop() { ColorState = stop0, Offset = 0 },
            new GradientStop() { ColorState = stop1, Offset = 1 }
        });

        GradientBrush = new LinearGradientBrush
        {
            GradientStops = new GradientStops
            {
                new Avalonia.Media.GradientStop(Colors.Black, 0),
                new Avalonia.Media.GradientStop(Colors.White, 1)
            }
        };
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        tabControl = e.NameScope.Find<TabControl>("PART_TabControl");
    }

    protected override void UpdateSelectedBrush()
    {
        if (SelectedTabIndex == 1)
        {
            SelectedBrush = GradientBrush;
        }
        else
        {
            base.UpdateSelectedBrush();
        }
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        if (tabControl != null && !EnableGradientsTab)
        {
            tabControl.FindDescendantOfType<TabItem>().IsVisible = false;
        }
    }

    private void UpdateGradientType()
    {
        var stops = GradientBrush.GradientStops;

        GradientBrush = GradientType switch
        {
            GradientType.Linear => new LinearGradientBrush { GradientStops = stops },
            GradientType.Radial => new RadialGradientBrush { GradientStops = stops },
            GradientType.Conic => new ConicGradientBrush { GradientStops = stops },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static void OnGradientChanged(AvaloniaPropertyChangedEventArgs<GradientBrush> args)
    {
        if (args.Sender is StandardColorPicker picker)
        {
            picker.UpdateSelectedBrush();
        }
    }

    private static void SelectedTabChanged(AvaloniaPropertyChangedEventArgs<int> args)
    {
        if (args.Sender is StandardColorPicker picker)
        {
            picker.UpdateSelectedBrush();
        }
    }

    private static void GradientTypeChanged(AvaloniaPropertyChangedEventArgs<GradientType> args)
    {
        if (args.Sender is StandardColorPicker picker)
        {
            picker.UpdateGradientType();
            picker.UpdateSelectedBrush();
        }
    }
}