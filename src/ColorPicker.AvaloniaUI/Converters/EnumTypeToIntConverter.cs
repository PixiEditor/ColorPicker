using System.Globalization;
using Avalonia.Data.Converters;
using ColorPicker.Models;

namespace ColorPicker.Converters;

internal class EnumTypeToIntConverter
    : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is Type targetEnumType)
        {
            return Enum.ToObject(targetEnumType, value);
        }

        return value;
    }
}