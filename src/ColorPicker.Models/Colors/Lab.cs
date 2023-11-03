namespace ColorPicker.Models.Colors;

internal struct Lab
{
    public double L { get; }
        
    public double a { get; }
        
    public double b { get; }

    public Lab(double l, double a, double b)
    {
        L = l;
        this.a = a;
        this.b = b;
    }
}