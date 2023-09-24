using ColorPicker.Models;
using System.Globalization;
using Avalonia.Data.Converters;

namespace ColorPicker.Converters
{
    class PickerTypeToIntConverter
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
}
