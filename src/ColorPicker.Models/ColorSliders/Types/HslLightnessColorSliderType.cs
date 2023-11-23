using System.Collections.Generic;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models.ColorSliders.Types;

internal class HslLightnessColorSliderType : IColorSliderType
{
    public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state)
    {
        return new List<ColorSliderGradientPoint>()
        {
            new ColorSliderGradientPoint(RgbHelper.HslToRgb(state.HSL_H, state.HSL_S, 0), 0),
            new ColorSliderGradientPoint(RgbHelper.HslToRgb(state.HSL_H, state.HSL_S, 0.25), 0.25),
            new ColorSliderGradientPoint(RgbHelper.HslToRgb(state.HSL_H, state.HSL_S, 0.5), 0.5),
            new ColorSliderGradientPoint(RgbHelper.HslToRgb(state.HSL_H, state.HSL_S, 0.75), 0.75),
            new ColorSliderGradientPoint(RgbHelper.HslToRgb(state.HSL_H, state.HSL_S, 1), 1)
        };
    }

    public bool RefreshGradient => true;
}