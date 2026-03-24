using System.Collections.Generic;
using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models.ColorSliders.Types
{
    internal class OkHsvHueColorSliderType : IColorSliderType
    {
        public List<ColorSliderGradientPoint> CalculateRgbGradient(ColorState state, bool enabled)
        {
            return new List<ColorSliderGradientPoint>()
            {
                GetPointAt(0, 0),
                GetPointAt(22, 22 / 360.0),
                GetPointAt(51, 51 / 360.00),
                GetPointAt(139, 139 / 360.0),
                GetPointAt(199, 199 / 360.0),
                GetPointAt(245, 245 / 360.0),
                GetPointAt(280, 280 / 360.0),
                GetPointAt(0, 1)
            };

            ColorSliderGradientPoint GetPointAt(int value, double position)
            {
                var rgb = RgbHelper.OkHsvToRgb(value, 1.0, 1.0);

                if (!enabled)
                    rgb = (Colors.Rgb)ColorSpaceHelper.RgbToGrayTuple(rgb.R, rgb.G, rgb.B);

                return new ColorSliderGradientPoint(rgb, position);
            }
        }

        public bool RefreshGradient => false;
    }
}