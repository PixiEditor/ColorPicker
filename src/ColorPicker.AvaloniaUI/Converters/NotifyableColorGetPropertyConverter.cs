using System.Globalization;
using Avalonia.Data.Converters;
using ColorPicker.Models;

namespace ColorPicker.Converters;

public class NotifyableColorGetPropertyConverter : IValueConverter
{
    private readonly Dictionary<string, Func<NotifyableColor, double>> getters = new();

    public NotifyableColorGetPropertyConverter()
    {
        getters["A"] = color => color.A;
        getters["HSV_H"] = color => color.HSV_H;
        getters["HSV_S"] = color => color.HSV_S;
        getters["HSV_V"] = color => color.HSV_V;
        getters["HSL_H"] = color => color.HSL_H;
        getters["HSL_S"] = color => color.HSL_S;
        getters["HSL_L"] = color => color.HSL_L;
        getters["RGB_R"] = color => color.RGB_R;
        getters["RGB_G"] = color => color.RGB_G;
        getters["RGB_B"] = color => color.RGB_B;
    }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is NotifyableColor color)
            if (parameter is string propertyName)
                if (getters.ContainsKey(propertyName))
                    return getters[propertyName](color);

        return 0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}