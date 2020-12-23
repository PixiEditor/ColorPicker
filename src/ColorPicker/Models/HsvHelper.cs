using System;
using System.Collections.Generic;
using System.Text;

namespace ColorPicker.Models
{
    internal static class HsvHelper
    {
        /// <summary>
        /// Converts RGB to HSV, return -1 for undefined channels
        /// </summary>
        /// <param name="r">Red channel</param>
        /// <param name="g">Green channel</param>
        /// <param name="b">Blue channel</param>
        /// <returns>Values in order: Hue (0-360 or -1), Saturation (0-1 or -1), Value (0-1)</returns>
        public static (double, double, double) RgbToHsv(double r, double g, double b)
        {
            double min, max, delta;
            double h, s, v;

            min = Math.Min(r, Math.Min(g, b));
            max = Math.Max(r, Math.Max(g, b));
            v = max;
            delta = max - min;
            if (max != 0)
                s = delta / max;
            else
            {
                //pure black
                s = -1;
                h = -1;
                return (h, s, v);
            }
            if (r == max)
                h = (g - b) / delta;       // between yellow & magenta
            else if (g == max)
                h = 2 + (b - r) / delta;   // between cyan & yellow
            else
                h = 4 + (r - g) / delta;   // between magenta & cyan
            h *= 60;
            if (h < 0)
                h += 360;
            if (Double.IsNaN(h)) //delta == 0, case of pure gray
                h = -1;

            return (h, s, v);
        }

        /// <summary>
        /// Converts HSV to RGB
        /// </summary>
        /// <param name="h">Hue, 0-360</param>
        /// <param name="s">Saturation, 0-1</param>
        /// <param name="v">Value, 0-1</param>
        /// <returns>Values (0-1) in order: R, G, B</returns>
        public static (double, double, double) HsvToRgb(double h, double s, double v)
        {
            if (s == 0)
            {
                // achromatic (grey)
                return (v, v, v);
            }
            if (h >= 360.0)
                h = 0;
            h /= 60;
            int i = (int)h;
            double f = h - i;
            double p = (v * (1 - s));
            double q = (v * (1 - s * f));
            double t = (v * (1 - s * (1 - f)));

            return i switch
            {
                0 => (v, t, p),
                1 => (q, v, p),
                2 => (p, v, t),
                3 => (p, q, v),
                4 => (t, p, v),
                _ => (v, p, q)
            };
        }
    }
}
