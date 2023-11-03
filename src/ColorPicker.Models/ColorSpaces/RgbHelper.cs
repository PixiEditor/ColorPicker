using System;

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
    public static Tuple<double, double, double> HsvToRgb(double h, double s, double v)
    {
        if (s == 0)
            // achromatic (grey)
            return new Tuple<double, double, double>(v, v, v);
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
            case 0: return new Tuple<double, double, double>(v, t, p);
            case 1: return new Tuple<double, double, double>(q, v, p);
            case 2: return new Tuple<double, double, double>(p, v, t);
            case 3: return new Tuple<double, double, double>(p, q, v);
            case 4: return new Tuple<double, double, double>(t, p, v);
            default: return new Tuple<double, double, double>(v, p, q);
        }
    }
    
    /// <summary>
    ///     Converts HSL to RGB
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="l">Lightness, 0-1</param>
    /// <returns>Values (0-1) in order: R, G, B</returns>
    public static Tuple<double, double, double> HslToRgb(double h, double s, double l)
    {
        var hueCircleSegment = (int)(h / 60);
        var circleSegmentFraction = (h - 60 * hueCircleSegment) / 60;

        var maxRGB = l < 0.5 ? l * (1 + s) : l + s - l * s;
        var minRGB = 2 * l - maxRGB;
        var delta = maxRGB - minRGB;

        switch (hueCircleSegment)
        {
            case 0:
                return new Tuple<double, double, double>(maxRGB, delta * circleSegmentFraction + minRGB,
                    minRGB); //red-yellow
            case 1:
                return new Tuple<double, double, double>(delta * (1 - circleSegmentFraction) + minRGB, maxRGB,
                    minRGB); //yellow-green
            case 2:
                return new Tuple<double, double, double>(minRGB, maxRGB,
                    delta * circleSegmentFraction + minRGB); //green-cyan
            case 3:
                return new Tuple<double, double, double>(minRGB, delta * (1 - circleSegmentFraction) + minRGB,
                    maxRGB); //cyan-blue
            case 4:
                return new Tuple<double, double, double>(delta * circleSegmentFraction + minRGB, minRGB,
                    maxRGB); //blue-purple
            default:
                return new Tuple<double, double, double>(maxRGB, minRGB,
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
    public static Tuple<double, double, double> OkHsvToRgb(double h, double s, double v)
    {
        var tuple = OkHelper.OkHsvToSrgb(h / 360, s, v);
        return new Tuple<double, double, double>(tuple.Item1, tuple.Item2, tuple.Item3);
    }
    
    /// <summary>
    ///     Converts OKHSL to RGB
    /// </summary>
    /// <param name="h">Hue, 0-360</param>
    /// <param name="s">Saturation, 0-1</param>
    /// <param name="l">Lightness, 0-1</param>
    /// <returns>Values (0-1) in order: R, G, B</returns>
    public static Tuple<double, double, double> OkHslToRgb(double h, double s, double l)
    {
        var tuple = OkHelper.OkHslToSrgb(h / 360.0, s, l);
        return new Tuple<double, double, double>(tuple.Item1, tuple.Item2, tuple.Item3);
    }
}