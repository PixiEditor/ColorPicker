using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ColorPicker.Converters
{
    [ValueConversion(typeof(Color), typeof(string))]
    internal class ColorToShortHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "#" + ((Color)value).ToString().Substring(3, 6);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = (string)value;
            text = Regex.Replace(text.ToUpperInvariant(), @"[^0-9A-F]", "");
            StringBuilder final = new StringBuilder();
            if (text.Length == 3) //short hex
            {
                final.Append("#FF").Append(text[0]).Append(text[0]).Append(text[1]).Append(text[1]).Append(text[2]).Append(text[2]);
            }
            else //regular hex
            {
                final.Append("#").Append(text);
            }
            try
            {
                return ColorConverter.ConvertFromString(final.ToString());
            }
            catch (Exception)
            {
                return DependencyProperty.UnsetValue;
            }
        }
    }
}
