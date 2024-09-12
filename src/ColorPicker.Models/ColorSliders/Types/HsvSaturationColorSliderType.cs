using System.Collections.Generic;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models.ColorSliders.Types;

internal class HsvSaturationColorSliderType : IColorSliderType
{
    public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state)
    {
        return new List<ColorSliderGradientPoint>()
        {
            new ColorSliderGradientPoint(RgbHelper.HsvToRgb(state.HSV_H, 0, state.HSV_V), 0),
            new ColorSliderGradientPoint(RgbHelper.HsvToRgb(state.HSV_H, 1, state.HSV_V), 1)
        };
    }

    public bool RefreshGradient => true;
}