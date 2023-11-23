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
}