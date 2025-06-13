using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace ColorPicker.Converters;

public class PointsToMarginConverter : IMultiValueConverter
{
    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Count < 3 || values[0] is not double x || values[1] is not double y || values[2] is not Rect rect)
        {
            return new Thickness(0);
        }

        Thickness margin = new Thickness(
            x * rect.Width,
            y * rect.Height,
            0, 0);

        return margin;
    }
}
