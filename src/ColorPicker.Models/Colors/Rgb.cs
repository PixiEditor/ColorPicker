using System;

namespace ColorPicker.Models.Colors;

public struct Rgb
{
    public double R { get; }
        
    public double G { get; }
        
    public double B { get; }

    public Rgb(double r, double g, double b)
    {
        R = r;
        G = g;
        B = b;
    }

    public static implicit operator Tuple<double, double, double>(Rgb rgb) => Tuple.Create(rgb.R, rgb.G, rgb.B);
}