using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace ColorPicker.Converters
{
    public class BoolToInvertedBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return !boolValue;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("ConvertBack() of BoolToInvertedBoolConverter is not implemented");
        }
    }
}
