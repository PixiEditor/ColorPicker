using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace ColorPicker.Converters;

public class OffsetToMarginConverter : IMultiValueConverter
{
    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Count != 2)
        {
            return new Thickness();
        }

        if (!(values[0] is double offset) || !(values[1] is double width))
        {
            return new Thickness();
        }

        double padding = parameter is double p ? p : 0;

        padding = offset < 0.5 ? 0 : -padding;

        return new Thickness(offset * width, 0, 0, 0) + new Thickness(padding, 0, 0, 0);
    }
}