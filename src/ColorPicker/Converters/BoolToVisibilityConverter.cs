using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ColorPicker.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    class BoolToVisibilityConverter : DependencyObject, IValueConverter
    {
        public static DependencyProperty InvertProperty =
            DependencyProperty.Register(nameof(Invert), typeof(bool), typeof(BoolToVisibilityConverter),
                new PropertyMetadata(false));

        public bool Invert
        {
            get => (bool)GetValue(InvertProperty);
            set => SetValue(InvertProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool actualValue = (bool)value ^ Invert;
            return actualValue ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
