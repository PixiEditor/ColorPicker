using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace ColorPicker.Converters;

public class PointComponentsToPointConverter : IMultiValueConverter
{
    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Count < 3 || values[0] is not double x || values[1] is not double y || values[2] is not Rect rect)
        {
            return new Point(0, 0);
        }

        Point point = new Point(
            x * rect.Width,
            y * rect.Height);

        return point;
    }
}
