using Avalonia;

namespace ColorPicker;

public class AlphaSlider : PickerControlBase
{
    public static readonly StyledProperty<double> SmallChangeProperty = AvaloniaProperty.Register<AlphaSlider, double>(
        nameof(SmallChange), 1.0);

    public double SmallChange
    {
        get => GetValue(SmallChangeProperty);
        set => SetValue(SmallChangeProperty, value);
    }
}