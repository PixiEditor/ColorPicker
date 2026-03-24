using System.Collections.Generic;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models.ColorSliders.Types
{
    internal class OkHslLightnessColorSliderType : IColorSliderType
    {
        public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state, bool enabled)
        {
            return new List<ColorSliderGradientPoint>()
            {
                GetPointAt(0),
                GetPointAt(0.25),
                GetPointAt(0.50),
                GetPointAt(0.75),
                GetPointAt(1)
            };

            ColorSliderGradientPoint GetPointAt(double value)
            {
                var rgb = RgbHelper.OkHslToRgb(state.OKHSL_H, state.OKHSL_S, value);

                if(!enabled)
                    rgb = (Colors.Rgb)ColorSpaceHelper.RgbToGrayTuple(rgb.R, rgb.G, rgb.B);

                return new ColorSliderGradientPoint(rgb, value);
            }
        }

        public bool RefreshGradient => true;
    }
}