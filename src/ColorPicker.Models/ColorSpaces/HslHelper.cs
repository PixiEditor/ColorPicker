using System;
using ColorPicker.Models.Colors;

namespace ColorPicker.Models.ColorSpaces;

public static class HslHelper
{
    /// <summary>
    ///     Converts RGB to HSL, returns -1 for undefined channels
    /// </summary>
    /// <param name="r">Red channel</param>
    /// <param name="b">Blue channel</param>
    /// <param name="g">Green channel</param>
    /// <returns>Values in order: Hue (0-360 or -1), Saturation (0-1 or -1), Lightness (0-1)</returns>
    public static Hsl RgbToHsl(double r, double g, double b)
    {
        double h, s, l;

        var min = Math.Min(Math.Min(r, g), b);
        var max = Math.Max(Math.Max(r, g), b);
        var delta = max - min;
        l = (max + min) / 2;

        if (max == 0)
            //pure black
            return new Hsl(-1, -1, 0);

        if (delta == 0)
            //gray
            return new Hsl(-1, 0, l);

        //magic
        s = l <= 0.5 ? delta / (max + min) : delta / (2 - max - min);

        if (r == max)
            h = (g - b) / 6 / delta;
        else if (g == max)
            h = 1.0f / 3 + (b - r) / 6 / delta;
        else
            h = 2.0f / 3 + (r - g) / 6 / delta;

        if (h < 0)
            h += 1;
        if (h > 1)
            h -= 1;

        h *= 360;

        return new Hsl(h, s, l);
    }
    
    /// <summary>
    ///     Converts HSV to HSL
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="v">Value, 0-1</param>
    /// <returns>Values in order: Hue (same), Saturation (0-1 or -1), Lightness (0-1)</returns>
    public static Hsl HsvToHsl(double h, double s, double v)
    {
        var hsl_l = v * (1 - s / 2);
        double hsl_s;
        if (hsl_l == 0 || hsl_l == 1)
            hsl_s = -1;
        else
            hsl_s = (v - hsl_l) / Math.Min(hsl_l, 1 - hsl_l);
        return new Hsl(h, hsl_s, hsl_l);
    }
    
    
    /// <summary>
    ///     Converts OKHSL to HSL
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="l">Lightness, 0-1</param>
    /// <returns>Values in order: Hue (0-360), Saturation (0-1), Lightness (0-1)</returns>
    public static Hsl OkHslToHsl(double h, double s, double l)
    {
        var rgb = RgbHelper.OkHslToRgb(h, s, l);
        return HslHelper.RgbToHsl(rgb.R, rgb.G, rgb.B);
    }
    
    /// <summary>
    ///     Converts OKHSV to HSL
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="v">Value, 0-1</param>
    /// <returns>Values in order: Hue (0-360), Saturation (0-1), Lightness (0-1)</returns>
    public static Hsl OkHsvToHsl(double h, double s, double v)
    {
        var rgb = RgbHelper.OkHsvToRgb(h, s, v);
        return HslHelper.RgbToHsl(rgb.R, rgb.G, rgb.B);
    }
}