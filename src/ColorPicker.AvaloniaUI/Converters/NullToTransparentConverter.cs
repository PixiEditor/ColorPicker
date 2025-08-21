using System.Globalization;
using Avalonia.Data.Converters;

namespace ColorPicker.Converters;

public class NullToTransparentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return Avalonia.Media.Brushes.Transparent;
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}
