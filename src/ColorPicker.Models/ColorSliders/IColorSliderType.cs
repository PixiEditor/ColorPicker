using System;
using System.Collections.Generic;

namespace ColorPicker.Models.ColorSliders;

public interface IColorSliderType
{
    List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState currentColorState);
    bool RefreshGradient { get; }
}