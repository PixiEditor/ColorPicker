using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Reactive;
using ColorPicker.Models;

namespace ColorPicker.Converters;

internal class ColorToHexConverter : AvaloniaObject, IValueConverter
{
    public static readonly StyledProperty<bool> ShowAlphaProperty =
        AvaloniaProperty.Register<ColorToHexConverter, bool>(
            nameof(ShowAlpha), true);
    
    public bool ShowAlpha
    {
        get => GetValue(ShowAlphaProperty);
        set => SetValue(ShowAlphaProperty, value);
    }

    
    public static readonly StyledProperty<HexRepresentationType> HexRepresentationProperty = 
        AvaloniaProperty.Register<PortableColorPicker, HexRepresentationType>(
            nameof(HexRepresentation), HexRepresentationType.RGBA);

    public HexRepresentationType HexRepresentation
    {
        get => GetValue(HexRepresentationProperty);
        set => SetValue(HexRepresentationProperty, value);
    }

    static ColorToHexConverter()
    {
        ShowAlphaProperty.Changed.Subscribe(
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs<bool>>(ShowAlphaChangedCallback));
        HexRepresentationProperty.Changed.Subscribe(
            new AnonymousObserver<AvaloniaPropertyChangedEventArgs<HexRepresentationType>>(HexRepresentationChangedCallback));
    }

    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Color c = (Color)value;
        return HexHelper.RgbaValuesToString(c.R, c.G, c.B, c.A, ShowAlpha, HexRepresentation) ?? AvaloniaProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string valueStr)
            return AvaloniaProperty.UnsetValue;

        Tuple<byte, byte, byte, byte> values = HexHelper.ParseInputtedHexStringToRgbaValues(valueStr, ShowAlpha, HexRepresentation);
        if (values is null)
            return AvaloniaProperty.UnsetValue;

        return Color.FromArgb(values.Item4, values.Item1, values.Item2, values.Item3);
    }

    public event EventHandler OnShowAlphaChange;

    public event EventHandler OnShowHexRepresentationChange;

    public void RaiseShowAlphaChange()
    {
        OnShowAlphaChange?.Invoke(this, EventArgs.Empty);
    }

    private static void ShowAlphaChangedCallback(AvaloniaPropertyChangedEventArgs<bool> d)
    {
        ((ColorToHexConverter)d.Sender).RaiseShowAlphaChange();
    }
    
    private void RaiseHexRepresentationChange()
    {
        OnShowHexRepresentationChange?.Invoke(this, EventArgs.Empty);
    }

    private static void HexRepresentationChangedCallback(AvaloniaPropertyChangedEventArgs<HexRepresentationType> d)
    {
        ((ColorToHexConverter)d.Sender).RaiseHexRepresentationChange();
    }
}