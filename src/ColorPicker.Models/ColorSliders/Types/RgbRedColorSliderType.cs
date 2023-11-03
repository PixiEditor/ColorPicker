using System.Collections.Generic;

namespace ColorPicker.Models.ColorSliders.Types;

internal class RgbRedColorSliderType : IColorSliderType
{
    public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state)
    {
        return new List<ColorSliderGradientPoint>()
        {
            new ColorSliderGradientPoint(0, state.RGB_G, state.RGB_B, 0.0),
            new ColorSliderGradientPoint(1, state.RGB_G, state.RGB_B, 1.0)
        };
    }

    public bool RefreshGradient => true;
}