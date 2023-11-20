using Avalonia;

namespace ColorPicker;

public class ColorSliders : PickerControlBase
{
    public static readonly StyledProperty<double> SmallChangeProperty =
        AvaloniaProperty.Register<ColorSliders, double>(
            nameof(SmallChange), 1.0);

    public static readonly StyledProperty<bool> ShowAlphaProperty = AvaloniaProperty.Register<ColorSliders, bool>(
        nameof(ShowAlpha), true);

    public static readonly StyledProperty<bool> ShowFractionalPartProperty =
        AvaloniaProperty.Register<ColorSliders, bool>(
            nameof(ShowFractionalPart), true);

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

    public bool ShowFractionalPart
    {
        get => GetValue(ShowFractionalPartProperty);
        set => SetValue(ShowFractionalPartProperty, value);
    }
}