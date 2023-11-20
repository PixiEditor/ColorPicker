﻿using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace ColorPicker.Converters;

internal class ColorToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var col = (Color)value;
        var c = Color.FromArgb(col.A, col.R, col.G, col.B);
        return new SolidColorBrush(c);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var c = (SolidColorBrush)value;
        var col = Color.FromArgb(c.Color.A, c.Color.R, c.Color.G, c.Color.B);
        return col;
    }
}