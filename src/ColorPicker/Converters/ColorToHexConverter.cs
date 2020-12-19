using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ColorPicker.Converters
{
    [ValueConversion(typeof(Color), typeof(string))]
    class ColorToHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Color)value).ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = (string)value;
            text = Regex.Replace(text.ToUpperInvariant(), @"[^0-9A-F]", "");
            StringBuilder final = new StringBuilder();
            if (text.Length == 3) //short hex with no alpha
            {
                final.Append("#FF").Append(text[0]).Append(text[0]).Append(text[1]).Append(text[1]).Append(text[2]).Append(text[2]);
            }
            else if (text.Length == 4) //short hex with alpha
            {
                final.Append("#").Append(text[0]).Append(text[0]).Append(text[1]).Append(text[1]).Append(text[2]).Append(text[2]).Append(text[3]).Append(text[3]);
            }
            else if (text.Length == 6) //hex with no alpha
            {
                final.Append("#FF").Append(text);
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
