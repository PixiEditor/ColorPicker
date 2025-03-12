using System.Globalization;
using Avalonia.Data.Converters;

namespace ColorPicker.Converters;

public class IsEnumValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is not null && value is not null)
        {
            return string.Equals(value.ToString(), parameter.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
