namespace ColorPicker.Models.Colors;

public readonly struct Hsv
{
    public double H { get; }
        
    public double S { get; }
        
    public double V { get; }

    public Hsv(double h, double s, double v)
    {
        H = h;
        S = s;
        V = v;
    }
}