using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ColorPicker.Converters
{
    class ChooseOneConverter : DependencyObject, IMultiValueConverter
    {
        public static DependencyProperty IndexProperty
            = DependencyProperty.Register(nameof(Index), typeof(int), typeof(ChooseOneConverter),
            new PropertyMetadata(0));

        public int Index
        {
            get => (int)GetValue(IndexProperty);
            set => SetValue(IndexProperty, value);
        }
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values[Index];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var arr = new object[Index + 1];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = Binding.DoNothing;
            arr[Index] = value;
            return arr;
        }
    }
}
