using System;
using ColorPicker.Models.Colors;

namespace ColorPicker.Models.ColorSpaces;

public static class RgbHelper
{
    /// <summary>
    ///     Converts HSV to RGB
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="v">Value, 0-1</param>
    /// <returns>Values (0-1) in order: R, G, B</returns>
    public static Rgb HsvToRgb(double h, double s, double v)
    {
        if (s == 0)
            // achromatic (grey)
            return new Rgb(v, v, v);
        if (h >= 360.0)
            h = 0;
        h /= 60;
        var i = (int)h;
        var f = h - i;
        var p = v * (1 - s);
        var q = v * (1 - s * f);
        var t = v * (1 - s * (1 - f));

        switch (i)
        {
            case 0: return new Rgb(v, t, p);
            case 1: return new Rgb(q, v, p);
            case 2: return new Rgb(p, v, t);
            case 3: return new Rgb(p, q, v);
            case 4: return new Rgb(t, p, v);
            default: return new Rgb(v, p, q);
        }
    }
    
    /// <summary>
    ///     Converts HSL to RGB
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="l">Lightness, 0-1</param>
    /// <returns>Values (0-1) in order: R, G, B</returns>
    public static Rgb HslToRgb(double h, double s, double l)
    {
        var hueCircleSegment = (int)(h / 60);
        var circleSegmentFraction = (h - 60 * hueCircleSegment) / 60;

        var maxRGB = l < 0.5 ? l * (1 + s) : l + s - l * s;
        var minRGB = 2 * l - maxRGB;
        var delta = maxRGB - minRGB;

        switch (hueCircleSegment)
        {
            case 0:
                return new Rgb(maxRGB, delta * circleSegmentFraction + minRGB,
                    minRGB); //red-yellow
            case 1:
                return new Rgb(delta * (1 - circleSegmentFraction) + minRGB, maxRGB,
                    minRGB); //yellow-green
            case 2:
                return new Rgb(minRGB, maxRGB,
                    delta * circleSegmentFraction + minRGB); //green-cyan
            case 3:
                return new Rgb(minRGB, delta * (1 - circleSegmentFraction) + minRGB,
                    maxRGB); //cyan-blue
            case 4:
                return new Rgb(delta * circleSegmentFraction + minRGB, minRGB,
                    maxRGB); //blue-purple
            default:
                return new Rgb(maxRGB, minRGB,
                    delta * (1 - circleSegmentFraction) + minRGB); //purple-red and invalid values
        }
    }

    /// <summary>
    ///     Converts OKHSV to RGB
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="v">Value, 0-1</param>
    /// <returns>Values (0-1) in order: R, G, B</returns>
    public static Rgb OkHsvToRgb(double h, double s, double v)
    {
        var rgb = OkHelper.OkHsvToSrgb(h / 360, s, v);
        return new Rgb(rgb.R, rgb.G, rgb.B);
    }
    
    /// <summary>
    ///     Converts OKHSL to RGB
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="l">Lightness, 0-1</param>
    /// <returns>Values (0-1) in order: R, G, B</returns>
    public static Rgb OkHslToRgb(double h, double s, double l)
    {
        var rgb = OkHelper.OkHslToSrgb(h / 360.0, s, l);
        return new Rgb(rgb.R, rgb.G, rgb.B);
    }
}