using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using ColorPicker.Models;

namespace ColorPicker;

public class PortableColorPicker : DualColorGradientPickerBase
{
    public static readonly StyledProperty<double> SmallChangeProperty =
        AvaloniaProperty.Register<PortableColorPicker, double>(
            nameof(SmallChange),
            1.0);

    public static readonly StyledProperty<bool> ShowAlphaProperty =
        AvaloniaProperty.Register<PortableColorPicker, bool>(
            nameof(ShowAlpha),
            true);

    public static readonly StyledProperty<PickerType> PickerTypeProperty =
        AvaloniaProperty.Register<PortableColorPicker, PickerType>(
            nameof(PickerType),
            PickerType.HSV);

    public static readonly StyledProperty<bool> ShowFractionalPartProperty =
        AvaloniaProperty.Register<PortableColorPicker, bool>(
            nameof(ShowFractionalPart),
            true);

    public static readonly StyledProperty<HexRepresentationType> HexRepresentationProperty =
        AvaloniaProperty.Register<PortableColorPicker, HexRepresentationType>(
            nameof(HexRepresentation), HexRepresentationType.RGBA);

    public static readonly StyledProperty<bool> ShowRecentColorsProperty =
        AvaloniaProperty.Register<PortableColorPicker, bool>(
            nameof(ShowRecentColors), true);

    public static readonly StyledProperty<bool> ShowRecentGradientsProperty =
        AvaloniaProperty.Register<PortableColorPicker, bool>(
            nameof(ShowRecentGradients), true);

    public bool ShowRecentGradients
    {
        get => GetValue(ShowRecentGradientsProperty);
        set => SetValue(ShowRecentGradientsProperty, value);
    }

    public bool ShowRecentColors
    {
        get => GetValue(ShowRecentColorsProperty);
        set => SetValue(ShowRecentColorsProperty, value);
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

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var popupPart = e.NameScope.Find<Popup>("popup");
        popupPart.Closed += PopupPartOnClosed;
        if (popupPart != null)
        {
            popupPart.PointerPressed += (sender, args) => { args.Handled = true; };
        }
    }

    private void PopupPartOnClosed(object sender, EventArgs e)
    {
        if (ShowRecentColors && SelectedBrush is ISolidColorBrush solidColorBrush)
        {
            RecentsStore.Global.TryAddRecentColor(SelectedColor);
        }
        else if (ShowRecentGradients && SelectedBrush is IGradientBrush gradientBrush)
        {
            RecentsStore.Global.TryAddRecentGradient(gradientBrush);
        }
    }
}
