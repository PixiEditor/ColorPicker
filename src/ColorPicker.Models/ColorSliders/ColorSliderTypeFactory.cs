using System;
using ColorPicker.Models.ColorSliders.Types;

namespace ColorPicker.Models.ColorSliders;

public static class ColorSliderTypeFactory
{
    public static IColorSliderType Get(ColorSliderType type)
    {
        switch (type)
        {
            case ColorSliderType.RgbRed:
                return new RgbRedColorSliderType();
            case ColorSliderType.RgbGreen:
                return new RgbGreenColorSliderType();
            case ColorSliderType.RgbBlue:
                return new RgbBlueColorSliderType();
            case ColorSliderType.Alpha:
                return new AlphaColorSliderType();
            case ColorSliderType.HsvHslHue:
                return new HsvHslHueColorSliderType();
            case ColorSliderType.HsvSaturation:
                return new HsvSaturationColorSliderType();
            case ColorSliderType.HsvValue:
                return new HsvValueColorSliderType();
            case ColorSliderType.HslSaturation:
                return new HslSaturationColorSliderType();
            case ColorSliderType.HslLightness:
                return new HslLightnessColorSliderType();
            case ColorSliderType.OkHsvHue:
                return new OkHsvHueColorSliderType();
            case ColorSliderType.OkHsvSaturation:
                return new OkHsvSaturationColorSliderType();
            case ColorSliderType.OkHsvValue:
                return new OkHsvValueColorSliderType();
            case ColorSliderType.OkHslHue:
                return new OkHslHueColorSliderType();
            case ColorSliderType.OkHslSaturation:
                return new OkHslSaturationColorSliderType();
            case ColorSliderType.OkHslLightness:
                return new OkHslLightnessColorSliderType();
            default:
                throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}