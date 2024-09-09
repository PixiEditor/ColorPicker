using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using ColorPicker.Models;

namespace ColorPicker;

public class PortableColorPicker : DualPickerControlBase
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
        if (popupPart != null)
        {
            popupPart.PointerPressed += (sender, args) =>
            {
                args.Handled = true;
            };
        }
    }
}