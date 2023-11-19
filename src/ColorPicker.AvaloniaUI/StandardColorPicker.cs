using Avalonia;
using ColorPicker.Models;

namespace ColorPicker;

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
}