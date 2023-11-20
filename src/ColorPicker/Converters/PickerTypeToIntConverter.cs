using System;
using System.Globalization;
using System.Windows.Data;
using ColorPicker.Models;

namespace ColorPicker.Converters
{
    [ValueConversion(typeof(PickerType), typeof(int))]
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
}