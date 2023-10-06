using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace ColorPicker.Converters;

public class DividedSizeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is CornerRadius radius)
        {
            var param = (double)parameter;
            return new CornerRadius(radius.TopLeft / param, radius.TopRight / param, radius.BottomRight / param,
                radius.BottomLeft / param);
        }

        return (double)value / (double)parameter;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}