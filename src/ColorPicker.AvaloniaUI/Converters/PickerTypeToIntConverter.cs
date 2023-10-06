using System.Globalization;
using Avalonia.Data.Converters;
using ColorPicker.Models;

namespace ColorPicker.Converters;

internal class PickerTypeToIntConverter
    : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (PickerType)value;
    }
}