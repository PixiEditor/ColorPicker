using System.Collections;
using System.Globalization;
using Avalonia.Data.Converters;

namespace ColorPicker.Converters;

public class LastItemOfConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not IList list || list.Count == 0)
        {
            return null;
        }

        return list[^1];
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
