using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace ColorPicker.Converters;

public class MultiplyConverter : IMultiValueConverter
{
    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Count < 2)
        {
            return AvaloniaProperty.UnsetValue;
        }

        double result = 1.0;
        foreach (var value in values)
        {
            if (value is double d)
            {
                result *= d;
            }
        }

        return result;
    }
}
