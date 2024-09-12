using System;
using ColorPicker.Models.Colors;

namespace ColorPicker.Models.ColorSpaces;

public static class HsvHelper
{
    /// <summary>
    ///     Converts HSL to HSV
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="l">Lightness, 0-1</param>
    /// <returns>Values in order: Hue (same), Saturation (0-1 or -1), Value (0-1)</returns>
    public static Hsv HslToHsv(double h, double s, double l)
    {
        var hsv_v = l + s * Math.Min(l, 1 - l);
        double hsv_s;
        if (hsv_v == 0)
            hsv_s = -1;
        else
            hsv_s = 2 * (1 - l / hsv_v);
        return new Hsv(h, hsv_s, hsv_v);
    }
    
    /// <summary>
    ///     Converts RGB to HSV, returns -1 for undefined channels
    /// </summary>
    /// <param name="r">Red channel</param>
    /// <param name="g">Green channel</param>
    /// <param name="b">Blue channel</param>
    /// <returns>Values in order: Hue (0-360 or -1), Saturation (0-1 or -1), Value (0-1)</returns>
    public static Hsv RgbToHsv(double r, double g, double b)
    {
        double min, max, delta;
        double h, s, v;

        min = Math.Min(r, Math.Min(g, b));
        max = Math.Max(r, Math.Max(g, b));
        v = max;
        delta = max - min;
        if (max != 0)
        {
            s = delta / max;
        }
        else
        {
            //pure black
            s = -1;
            h = -1;
            return new Hsv(h, s, v);
        }

        if (r == max)
            h = (g - b) / delta; // between yellow & magenta
        else if (g == max)
            h = 2 + (b - r) / delta; // between cyan & yellow
        else
            h = 4 + (r - g) / delta; // between magenta & cyan
        h *= 60;
        if (h < 0)
            h += 360;
        if (double.IsNaN(h)) //delta == 0, case of pure gray
            h = -1;

        return new Hsv(h, s, v);
    }

    /// <summary>
    ///     Converts OKHSL to HSV
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="l">Lightness, 0-1</param>
    /// <returns>Values in order: Hue (0-360), Saturation (0-1), Value (0-1)</returns>
    public static Hsv OkHslToHsv(double h, double s, double l)
    {
        var rgb = RgbHelper.OkHslToRgb(h, s, l);
        return HsvHelper.RgbToHsv(rgb.R, rgb.G, rgb.B);
    }
    
    /// <summary>
    ///     Converts OKHSV to HSV
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="v">Value, 0-1</param>
    /// <returns>Values in order: Hue (0-360), Saturation (0-1), Value (0-1)</returns>
    public static Hsv OkHsvToHsv(double h, double s, double v)
    {
        var rgb = RgbHelper.OkHsvToRgb(h, s, v);
        return HsvHelper.RgbToHsv(rgb.R, rgb.G, rgb.B);
    }
}