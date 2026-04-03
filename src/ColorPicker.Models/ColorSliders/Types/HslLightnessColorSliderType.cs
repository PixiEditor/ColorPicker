using System.Collections.Generic;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models.ColorSliders.Types
{
    internal class HslLightnessColorSliderType : IColorSliderType
    {
        public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state, bool enabled)
        {
            return new List<ColorSliderGradientPoint>()
            {
                GetPointAt(0),
                GetPointAt(0.25),
                GetPointAt(0.5),
                GetPointAt(0.75),
                GetPointAt(1)
            };

            ColorSliderGradientPoint GetPointAt(double value)
            {
                if (enabled)
                    return new ColorSliderGradientPoint(RgbHelper.HslToRgb(state.HSL_H, state.HSL_S, value), value);
                else
                    return new ColorSliderGradientPoint((Colors.Rgb)ColorSpaceHelper.HslToGray(state.HSL_H, state.HSL_S, value), value);
            }
        }

        public bool RefreshGradient => true;
    }
}