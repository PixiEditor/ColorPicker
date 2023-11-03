using System;

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
    
    public ColorSliderGradientPoint(Tuple<double, double, double> rgb, double position)
    {
        R = rgb.Item1;
        G = rgb.Item2;
        B = rgb.Item3;
        Position = position;
    }
}