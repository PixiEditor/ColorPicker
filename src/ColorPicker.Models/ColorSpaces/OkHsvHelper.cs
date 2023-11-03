using System;
using ColorPicker.Models.Colors;

namespace ColorPicker.Models.ColorSpaces;

public static class OkHsvHelper
{
    /// <summary>
    ///     Converts RGB to OKHSV, returns -1 for undefined channels
    /// </summary>
    /// <param name="r">Red channel</param>
    /// <param name="g">Green channel</param>
    /// <param name="b">Blue channel</param>
    /// <returns>Values in order: Hue (0-360 or -1), Saturation (0-1 or -1), Value (0-1)</returns>
    public static Hsv RgbToOkHsv(double r, double g, double b)
    {
        var hsl = OkHelper.SrgbToOkHsl(r, g, b);
        return new Hsv(hsl.H * 360, hsl.S, hsl.L);
    }
    
    /// <summary>
    ///     Converts HSL to OKHSV
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="l">Lightness, 0-1</param>
    /// <returns>Values in order: Hue (0-360), Saturation (0-1), Value (0-1)</returns>
    public static Hsv HslToOkHsv(double h, double s, double l)
    {
        var rgb = RgbHelper.HslToRgb(h, s, l);
        return OkHsvHelper.RgbToOkHsv(rgb.R, rgb.G, rgb.B);
    }
    
    /// <summary>
    ///     Converts HSV to OKHSV
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="v">Value, 0-1</param>
    /// <returns>Values in order: Hue (0-360), Saturation (0-1), Value (0-1)</returns>
    public static Hsv HsvToOkHsv(double h, double s, double v)
    {
        var rgb = RgbHelper.HsvToRgb(h, s, v);
        return OkHsvHelper.RgbToOkHsv(rgb.R, rgb.G, rgb.B);
    }

    /// <summary>
    ///     Converts OKHSL to OKHSV
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="l">Lightness, 0-1</param>
    /// <returns>Values in order: Hue (0-360), Saturation (0-1), Value (0-1)</returns>
    public static Hsv OkHslToOkHsv(double h, double s, double l)
    {
        var rgb = RgbHelper.OkHslToRgb(h, s, l);
        return OkHsvHelper.RgbToOkHsv(rgb.R, rgb.G, rgb.B);
    }
}