using System.Collections.Generic;

namespace ColorPicker.Models.ColorSliders.Types;

internal class AlphaColorSliderType : IColorSliderType
{
    public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state)
    {
        return new List<ColorSliderGradientPoint>()
        {
            new ColorSliderGradientPoint(state.RGB_R, state.RGB_G, state.RGB_B, 0.0) { A = 0 },
            new ColorSliderGradientPoint(state.RGB_R, state.RGB_G, state.RGB_B, 1.0) { A = 1 }
        };
    }

    public bool RefreshGradient => true;
}