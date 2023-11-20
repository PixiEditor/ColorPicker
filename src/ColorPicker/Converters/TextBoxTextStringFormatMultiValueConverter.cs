using System;
using System.Globalization;
using System.Windows.Data;

namespace ColorPicker.Converters
{
    public class TextBoxTextStringFormatMultiValueConverter : IMultiValueConverter
    {
        private readonly string numericFormat = "N1";

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2 || !(values[0] is double doubleVal) || !(values[1] is bool showFractionalPart))
                throw new ArgumentException("Values array should contain 2 elements", nameof(values));

            return doubleVal.ToString(showFractionalPart ? numericFormat : "N0");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (!double.TryParse(value.ToString().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture,
                    out var result)) return new[] { Binding.DoNothing, Binding.DoNothing };

            return new[] { result.ToString(numericFormat), Binding.DoNothing };
        }
    }
}