using System.Collections.Generic;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models.ColorSliders.Types;

internal class OkHsvValueColorSliderType : IColorSliderType
{
    public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state)
    {
        return new List<ColorSliderGradientPoint>()
        {
            new ColorSliderGradientPoint(RgbHelper.OkHsvToRgb(state.OKHSV_H, state.OKHSV_S, 0), 0),
            new ColorSliderGradientPoint(RgbHelper.OkHsvToRgb(state.OKHSV_H, state.OKHSV_S, 1), 1)
        };
    }

    public bool RefreshGradient => true;
}