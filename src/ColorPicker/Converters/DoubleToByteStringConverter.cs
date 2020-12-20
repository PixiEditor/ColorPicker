using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

namespace ColorPicker.Converters
{
    [ValueConversion(typeof(double), typeof(string))]
    internal class DoubleToByteStringConverter : DependencyObject, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Floor((double)value * 255).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = (string)value;
            bool percent = text.EndsWith("%");
            text = Regex.Replace(text, @"[^0-9.,-]", "").Replace(',', '.');

            if (text.StartsWith("."))
                text = "0" + text;
            if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double result))
                return DependencyProperty.UnsetValue;

            if (result <= 0)
                return 0;
            if (percent)
                return Math.Clamp(result, 0, 100) / 100;
            if (result < 1)
                return result;

            return Math.Clamp(result / 255, 0, 1);
        }
    }
}
