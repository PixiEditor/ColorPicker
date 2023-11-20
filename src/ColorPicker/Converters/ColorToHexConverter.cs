using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using ColorPicker.Models;

namespace ColorPicker.Converters
{
    [ValueConversion(typeof(Color), typeof(string))]
    internal class ColorToHexConverter : DependencyObject, IValueConverter
    {
        public static DependencyProperty ShowAlphaProperty =
            DependencyProperty.Register(nameof(ShowAlpha), typeof(bool), typeof(ColorToHexConverter),
                new PropertyMetadata(true, ShowAlphaChangedCallback));
        
        public static readonly DependencyProperty HexRepresentationProperty = 
            DependencyProperty.Register(nameof(HexRepresentation), typeof(HexRepresentationType), typeof(ColorToHexConverter),
                new PropertyMetadata(HexRepresentationType.RGBA, HexRepresentationChangedCallback));

        public HexRepresentationType HexRepresentation
        {
            get => (HexRepresentationType)GetValue(HexRepresentationProperty);
            set => SetValue(HexRepresentationProperty, value);
        }

        public bool ShowAlpha
        {
            get => (bool)GetValue(ShowAlphaProperty);
            set => SetValue(ShowAlphaProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color c = (Color)value;
            return HexHelper.RgbaValuesToString(c.R, c.G, c.B, c.A, ShowAlpha, HexRepresentation) ?? DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
                return DependencyProperty.UnsetValue;

            var values = HexHelper.ParseInputtedHexStringToRgbaValues((string)value, ShowAlpha, HexRepresentation);
            if (values is null)
                return DependencyProperty.UnsetValue;

            return Color.FromArgb(values.Item4, values.Item1, values.Item2, values.Item3);
        }

        public event EventHandler OnShowAlphaChange;
        public event EventHandler OnShowHexRepresentationChange;

        private void RaiseShowAlphaChange()
        {
            OnShowAlphaChange?.Invoke(this, EventArgs.Empty);
        }

        private static void ShowAlphaChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColorToHexConverter)d).RaiseShowAlphaChange();
        }
        
        private void RaiseHexRepresentationChange()
        {
            OnShowHexRepresentationChange?.Invoke(this, EventArgs.Empty);
        }

        private static void HexRepresentationChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColorToHexConverter)d).RaiseHexRepresentationChange();
        }
    }
}