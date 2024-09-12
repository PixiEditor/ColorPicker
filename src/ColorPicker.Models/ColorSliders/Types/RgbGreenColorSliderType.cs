using System.Collections.Generic;

namespace ColorPicker.Models.ColorSliders.Types;

internal class RgbGreenColorSliderType : IColorSliderType
{
    public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state)
    {
        return new List<ColorSliderGradientPoint>()
        {
            new ColorSliderGradientPoint(state.RGB_R, 0, state.RGB_B, 0.0),
            new ColorSliderGradientPoint(state.RGB_R, 1, state.RGB_B, 1.0)
        };
    }

    public bool RefreshGradient => true;
}