using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace ColorPicker.Converters;

public class GradientStopToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is GradientStop stop)
        {
            return stop.Color;
        }

        return Colors.Transparent;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
