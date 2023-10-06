using Avalonia;

namespace ColorPicker;

public class HexColorTextBox : PickerControlBase
{
    public static readonly StyledProperty<bool> ShowAlphaProperty = AvaloniaProperty.Register<HexColorTextBox, bool>(
        nameof(ShowAlpha), true);

    public bool ShowAlpha
    {
        get => GetValue(ShowAlphaProperty);
        set => SetValue(ShowAlphaProperty, value);
    }
}