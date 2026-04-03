using System.Collections.Generic;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models.ColorSliders.Types
{
    internal class HsvHslHueColorSliderType : IColorSliderType
    {
        public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state, bool enabled)
        {
            return new List<ColorSliderGradientPoint>()
            {
                GetPointAtHue(0, 0),
                GetPointAtHue(60, 1 / 6.0),
                GetPointAtHue(120, 2 / 6.0),
                GetPointAtHue(180, 0.5),
                GetPointAtHue(240, 4 / 6.0),
                GetPointAtHue(300, 5 / 6.0),
                GetPointAtHue(0, 1)
            };

            ColorSliderGradientPoint GetPointAtHue(int value, double position)
            {
                if (enabled)
                    return new ColorSliderGradientPoint(RgbHelper.HsvToRgb(value, 1.0, 1.0), position);
                else
                    return new ColorSliderGradientPoint((Colors.Rgb)ColorSpaceHelper.HsvToGray(value, 1.0, 1.0), position);
            }
        }

        public bool RefreshGradient => false;
    }
}