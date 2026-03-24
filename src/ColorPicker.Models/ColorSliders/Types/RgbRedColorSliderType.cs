using System.Collections.Generic;

namespace ColorPicker.Models.ColorSliders.Types
{
    internal class RgbRedColorSliderType : IColorSliderType
    {
        public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state, bool enabled)
        {
            return new List<ColorSliderGradientPoint>()
            {
                GetPointAt(0),
                GetPointAt(1)
            };

            ColorSliderGradientPoint GetPointAt(double value)
            {
                if (enabled)
                    return new ColorSliderGradientPoint(value, state.RGB_G, state.RGB_B, value);
                else
                    return new ColorSliderGradientPoint((Colors.Rgb)ColorSpaceHelper.RgbToGrayTuple(value, state.RGB_G, state.RGB_B), value);
            }
        }

        public bool RefreshGradient => true;
    }
}