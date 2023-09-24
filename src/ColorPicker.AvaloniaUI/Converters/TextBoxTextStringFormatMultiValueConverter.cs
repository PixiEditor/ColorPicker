using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace ColorPicker.Converters
{
    public class TextBoxTextStringFormatMultiValueConverter : IMultiValueConverter
    {
        private readonly string numericFormat = "N1";

        public object Convert(IList<object?> values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values is not [double doubleVal, bool showFractionalPart])
            {
                throw new ArgumentException("Values array should contain 2 elements", nameof(values));
            }

            return doubleVal.ToString(showFractionalPart ? numericFormat : "N0");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (!double.TryParse((value.ToString()).Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out double result))
            {
                return new[] { AvaloniaProperty.UnsetValue, AvaloniaProperty.UnsetValue };
            }

            return new[] { result.ToString(numericFormat), AvaloniaProperty.UnsetValue };
        }
    }
}