using System.Collections.Generic;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models.ColorSliders.Types;

internal class HsvValueColorSliderType : IColorSliderType
{
    public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state)
    {
        return new List<ColorSliderGradientPoint>()
        {
            new ColorSliderGradientPoint(RgbHelper.HsvToRgb(state.HSV_H, state.HSV_S, 0), 0),
            new ColorSliderGradientPoint(RgbHelper.HsvToRgb(state.HSV_H, state.HSV_S, 1), 1)
        };
    }

    public bool RefreshGradient => true;
}