using System.Collections.Generic;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models.ColorSliders.Types;

internal class HslSaturationColorSliderType : IColorSliderType
{
    public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state)
    {
        return new List<ColorSliderGradientPoint>()
        {
            new ColorSliderGradientPoint(RgbHelper.HslToRgb(state.HSL_H, 0, state.HSL_L), 0),
            new ColorSliderGradientPoint(RgbHelper.HslToRgb(state.HSL_H, 1, state.HSL_L), 1)
        };
    }

    public bool RefreshGradient => true;
}