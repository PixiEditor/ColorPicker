using System.Collections.Generic;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models.ColorSliders.Types;

internal class HsvHslHueColorSliderType : IColorSliderType
{
    public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state)
    {
        return new List<ColorSliderGradientPoint>()
        {
            GetPointAtHue(0, 0),
            GetPointAtHue(60, 1 / 6.0),
            GetPointAtHue(120, 2 / 6.0),
            GetPointAtHue(180, 0.5),
            GetPointAtHue(240, 4 / 6.0),
            GetPointAtHue(300, 5 / 6.0),
            GetPointAtHue(0, 1)
        };
    }
    
    private ColorSliderGradientPoint GetPointAtHue(int value, double position)
    {
        var rgbTuple = RgbHelper.HsvToRgb(value, 1.0, 1.0);
        return new ColorSliderGradientPoint(rgbTuple, position);
    }

    public bool RefreshGradient => false;
}