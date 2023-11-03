using System.Collections.Generic;

namespace ColorPicker.Models.ColorSliders.Types;

internal class RgbBlueColorSliderType : IColorSliderType
{
    public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state)
    {
        return new List<ColorSliderGradientPoint>()
        {
            new ColorSliderGradientPoint(state.RGB_R, state.RGB_G, 0, 0.0),
            new ColorSliderGradientPoint(state.RGB_R, state.RGB_G, 1, 1.0)
        };
    }

    public bool RefreshGradient => true;
}