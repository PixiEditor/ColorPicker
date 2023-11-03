using System;

namespace ColorPicker.Models.Colors;

public struct Hsl
{
    public double H { get; }
        
    public double S { get; }
        
    public double L { get; }

    public Hsl(double h, double s, double l)
    {
        H = h;
        S = s;
        L = l;
    }

    public static implicit operator Tuple<double, double, double>(Hsl rgb) => Tuple.Create(rgb.H, rgb.S, rgb.L);
}