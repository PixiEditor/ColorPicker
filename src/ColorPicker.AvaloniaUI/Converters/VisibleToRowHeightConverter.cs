using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace ColorPicker.Converters;

public class VisibleToRowHeightConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b)
            return b ? 20 : 0;

        return 0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}