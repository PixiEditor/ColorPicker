using System.Collections.Generic;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models.ColorSliders.Types;

internal class OkHsvSaturationColorSliderType : IColorSliderType
{
    public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state)
    {
        return new List<ColorSliderGradientPoint>()
        {
            new ColorSliderGradientPoint(RgbHelper.OkHsvToRgb(state.OKHSV_H, 0, state.OKHSV_V), 0),
            new ColorSliderGradientPoint(RgbHelper.OkHsvToRgb(state.OKHSV_H, 1, state.OKHSV_V), 1)
        };
    }

    public bool RefreshGradient => true;
}