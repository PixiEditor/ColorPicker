using System;

namespace ColorPicker.Models.ColorSpaces;

public static class OkHslHelper
{
    /// <summary>
    ///     Converts RGB to OKHSL, returns -1 for undefined channels
    /// </summary>
    /// <param name="r">Red channel</param>
    /// <param name="b">Blue channel</param>
    /// <param name="g">Green channel</param>
    /// <returns>Values in order: Hue (0-360 or -1), Saturation (0-1 or -1), Lightness (0-1)</returns>
    public static Tuple<double, double, double> RgbToOkHsl(double r, double g, double b)
    {
        var tuple = OkHelper.SrgbToOkHsl(r, g, b);
        return new Tuple<double, double, double>(tuple.Item1 * 360, tuple.Item2, tuple.Item3);
    } 
    
    /// <summary>
    ///     Converts HSL to OKHSL
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="l">Lightness, 0-1</param>
    /// <returns>Values in order: Hue (0-360), Saturation (0-1), Lightness (0-1)</returns>
    public static Tuple<double, double, double> HslToOkHsl(double h, double s, double l)
    {
        var rgb = RgbHelper.HslToRgb(h, s, l);
        return OkHslHelper.RgbToOkHsl(rgb.Item1, rgb.Item2, rgb.Item3);
    }
    
    /// <summary>
    ///     Converts HSV to OKHSL
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="v">Value, 0-1</param>
    /// <returns>Values in order: Hue (0-360), Saturation (0-1), Lightness (0-1)</returns>
    public static Tuple<double, double, double> HsvToOkHsl(double h, double s, double v)
    {
        var rgb = RgbHelper.HsvToRgb(h, s, v);
        return OkHslHelper.RgbToOkHsl(rgb.Item1, rgb.Item2, rgb.Item3);
    }
    
    /// <summary>
    ///     Converts OKHSV to OKHSL
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="v">Value, 0-1</param>
    /// <returns>Values in order: Hue (0-360), Saturation (0-1), Lightness (0-1)</returns>
    public static Tuple<double, double, double> OkHsvToOkHsl(double h, double s, double v)
    {
        var rgb = RgbHelper.OkHsvToRgb(h, s, v);
        return OkHslHelper.RgbToOkHsl(rgb.Item1, rgb.Item2, rgb.Item3);
    }
}