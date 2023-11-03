using System.Collections.Generic;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models.ColorSliders.Types;

internal class OkHslLightnessColorSliderType : IColorSliderType
{
    public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state)
    {
        return new List<ColorSliderGradientPoint>()
        {
            new ColorSliderGradientPoint(RgbHelper.OkHslToRgb(state.OKHSL_H, state.OKHSL_S, 0), 0),
            new ColorSliderGradientPoint(RgbHelper.OkHslToRgb(state.OKHSL_H, state.OKHSL_S, 0.25), 0.25),
            new ColorSliderGradientPoint(RgbHelper.OkHslToRgb(state.OKHSL_H, state.OKHSL_S, 0.50), 0.50),
            new ColorSliderGradientPoint(RgbHelper.OkHslToRgb(state.OKHSL_H, state.OKHSL_S, 0.75), 0.75),
            new ColorSliderGradientPoint(RgbHelper.OkHslToRgb(state.OKHSL_H, state.OKHSL_S, 1), 1)
        };
    }

    public bool RefreshGradient => true;
}