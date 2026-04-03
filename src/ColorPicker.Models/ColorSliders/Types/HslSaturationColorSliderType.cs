using System.Collections.Generic;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models.ColorSliders.Types
{
    internal class HslSaturationColorSliderType : IColorSliderType
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
                    return new ColorSliderGradientPoint(RgbHelper.HslToRgb(state.HSL_H, value, state.HSL_L), value);
                else
                    return new ColorSliderGradientPoint((Colors.Rgb)ColorSpaceHelper.HslToGray(state.HSL_H, 0, state.HSL_L), value);
            }
        }

        public bool RefreshGradient => true;
    }
}