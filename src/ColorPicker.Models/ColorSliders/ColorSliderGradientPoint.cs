using System;
using ColorPicker.Models.Colors;

namespace ColorPicker.Models.ColorSliders;

public struct ColorSliderGradientPoint
{
    public double R;
    public double G;
    public double B;
    public double A = 1.0;
    public double Position;

    public ColorSliderGradientPoint(double r, double g, double b, double position)
    {
        R = r;
        G = g;
        B = b;
        Position = position;
    }
    
    public ColorSliderGradientPoint(Rgb rgb, double position)
    {
        R = rgb.R;
        G = rgb.G;
        B = rgb.B;
        Position = position;
    }
}