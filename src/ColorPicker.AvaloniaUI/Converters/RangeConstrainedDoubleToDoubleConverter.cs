using ColorPicker.Models;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace ColorPicker.Converters
{
    internal class RangeConstrainedDoubleToDoubleConverter : AvaloniaObject, IValueConverter
    {
        public static readonly StyledProperty<double> MinProperty = AvaloniaProperty.Register<RangeConstrainedDoubleToDoubleConverter, double>(
            nameof(Min), 0);

        public static readonly StyledProperty<double> MaxProperty = AvaloniaProperty.Register<RangeConstrainedDoubleToDoubleConverter, double>(
            nameof(Max), 1);

        public double Min
        {
            get => (double)GetValue(MinProperty);
            set => SetValue(MinProperty, value);
        }

        public double Max
        {
            get => (double)GetValue(MaxProperty);
            set => SetValue(MaxProperty, value);
        }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null) return AvaloniaProperty.UnsetValue;
            if (!double.TryParse(((string)value).Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out double result))
                return AvaloniaProperty.UnsetValue;
            return MathHelper.Clamp(result, Min, Max);
        }
    }
}
