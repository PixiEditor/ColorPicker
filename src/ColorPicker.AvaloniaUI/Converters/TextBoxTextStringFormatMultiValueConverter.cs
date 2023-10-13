using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace ColorPicker.Converters;

public class TextBoxTextStringFormatMultiValueConverter : IMultiValueConverter
{
    private readonly string numericFormat = "N1";

    public object Convert(IList<object?> values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Count != 2)
            throw new ArgumentException("Values array should contain 2 elements", nameof(values));

        double doubleVal = values[0] is double d ? d : double.Parse(values[0].ToString());
        bool showFractionalPart = values[1] is true;

        return doubleVal.ToString(showFractionalPart ? numericFormat : "N0");
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        if (!double.TryParse(value.ToString().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture,
                out var result)) return new[] { AvaloniaProperty.UnsetValue, AvaloniaProperty.UnsetValue };

        return new[] { result.ToString(numericFormat), AvaloniaProperty.UnsetValue };
    }
}