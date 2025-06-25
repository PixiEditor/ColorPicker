using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using ColorPicker.Behaviors;
using ColorPicker.Models;

namespace ColorPicker.Converters;

internal class RangeConstrainedDoubleToDoubleConverter : AvaloniaObject, IValueConverter
{
    public static readonly StyledProperty<double> OriginalValueProperty =
        AvaloniaProperty.Register<RangeConstrainedDoubleToDoubleConverter, double>("OriginalValue");

    public static readonly StyledProperty<double> MinProperty =
        AvaloniaProperty.Register<RangeConstrainedDoubleToDoubleConverter, double>(
            nameof(Min));

    public static readonly StyledProperty<double> MaxProperty =
        AvaloniaProperty.Register<RangeConstrainedDoubleToDoubleConverter, double>(
            nameof(Max), 1);

    public double Min
    {
        get => GetValue(MinProperty);
        set => SetValue(MinProperty, value);
    }

    public double Max
    {
        get => GetValue(MaxProperty);
        set => SetValue(MaxProperty, value);
    }

    public static readonly StyledProperty<bool> ShowFractionalPartProperty =
        AvaloniaProperty.Register<RangeConstrainedDoubleToDoubleConverter, bool>(
            nameof(ShowFractionalPart), true);

    public bool ShowFractionalPart
    {
        get => GetValue(ShowFractionalPartProperty);
        set => SetValue(ShowFractionalPartProperty, value);
    }

    public double OriginalValue
    {
        get { return (double)GetValue(OriginalValueProperty); }
        set { SetValue(OriginalValueProperty, value); }
    }


    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double doubleValue)
        {
            return doubleValue.ToString(ShowFractionalPart ? "N1" : "N0");
        }

        return value.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return AvaloniaProperty.UnsetValue;
        if (!double.TryParse(((string)value).Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture,
                out var result))
            return AvaloniaProperty.UnsetValue;

        var clampedValue = MathHelper.Clamp(result, Min, Max);

        if (Math.Abs(clampedValue - OriginalValue) < 0.1)
        {
            return OriginalValue;
        }

        return clampedValue;
    }
}
