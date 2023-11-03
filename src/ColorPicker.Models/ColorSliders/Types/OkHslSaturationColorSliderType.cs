using System.Collections.Generic;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models.ColorSliders.Types;

internal class OkHslSaturationColorSliderType : IColorSliderType
{
    public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state)
    {
        return new List<ColorSliderGradientPoint>()
        {
            new ColorSliderGradientPoint(RgbHelper.OkHslToRgb(state.OKHSL_H, 0, state.OKHSL_L), 0),
            new ColorSliderGradientPoint(RgbHelper.OkHslToRgb(state.OKHSL_H, 1, state.OKHSL_L), 1)
        };
    }

    public bool RefreshGradient => true;
}