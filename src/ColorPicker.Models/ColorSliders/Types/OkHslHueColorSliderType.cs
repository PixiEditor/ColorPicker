using System.Collections.Generic;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models.ColorSliders.Types;

internal class OkHslHueColorSliderType : IColorSliderType
{
    public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state)
    {
        return new List<ColorSliderGradientPoint>()
        {
            GetPointAtHue(0, 0),
            GetPointAtHue(22, 22 / 360.0),
            GetPointAtHue(51, 51 / 360.00),
            GetPointAtHue(139, 139 / 360.0),
            GetPointAtHue(199, 199 / 360.0),
            GetPointAtHue(245, 245 / 360.0),
            GetPointAtHue(280, 280 / 360.0),
            GetPointAtHue(0, 1)
        };
    }

    private ColorSliderGradientPoint GetPointAtHue(int value, double position)
    {
        var rgbTuple = RgbHelper.OkHslToRgb(value, 1.0, 0.62);
        return new ColorSliderGradientPoint(rgbTuple, position);
    }

    public bool RefreshGradient => false;
}