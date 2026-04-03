using System.Collections.Generic;

namespace ColorPicker.Models.ColorSliders.Types
{
    internal class AlphaColorSliderType : IColorSliderType
    {
        public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state, bool enabled)
        {
            return new List<ColorSliderGradientPoint>()
            {
                GetPointAt(0),
                GetPointAt(1)
            };

            ColorSliderGradientPoint GetPointAt(double value)
            {
                if (enabled)
                    return new ColorSliderGradientPoint(state.RGB_R, state.RGB_G, state.RGB_B, value) { A = value };
                else
                    return new ColorSliderGradientPoint((Colors.Rgb)ColorSpaceHelper.RgbToGrayTuple(state.RGB_R, state.RGB_G, state.RGB_B), value) { A = value };
            }
        }

        public bool RefreshGradient => true;
    }
}