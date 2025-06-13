using System.Globalization;
using Avalonia.Data.Converters;

namespace ColorPicker.Converters;

internal class EqualsConverter : IMultiValueConverter
{
    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Count != 2)
        {
            return false;
        }

        return values[0]?.Equals(values[1]) ?? false;
    }
}