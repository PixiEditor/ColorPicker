using System.Collections.Generic;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models.ColorSliders.Types
{
    internal class HsvSaturationColorSliderType : IColorSliderType
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
                    return new ColorSliderGradientPoint(RgbHelper.HsvToRgb(state.HSV_H, value, state.HSV_V), value);
                else
                    return new ColorSliderGradientPoint((Colors.Rgb)ColorSpaceHelper.HsvToGray(state.HSV_H, value, state.HSV_V), value);
            }
        }

        public bool RefreshGradient => true;
    }
}