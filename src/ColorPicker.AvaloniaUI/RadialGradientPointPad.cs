using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Input;

namespace ColorPicker;

[TemplatePart("PART_MainHandle", typeof(Ellipse))]
public class RadialGradientPointPad : GradientPad, INotifyPropertyChanged
{
    public static readonly StyledProperty<double> RadiusProperty =
        AvaloniaProperty.Register<RadialGradientPointPad, double>(
            nameof(Radius));

    public static readonly StyledProperty<double> CenterXProperty =
        AvaloniaProperty.Register<RadialGradientPointPad, double>(
            nameof(CenterX));

    public static readonly StyledProperty<double> CenterYProperty =
        AvaloniaProperty.Register<RadialGradientPointPad, double>(
            nameof(CenterY));

    public double CenterY
    {
        get => GetValue(CenterYProperty);
        set => SetValue(CenterYProperty, value);
    }

    public double CenterX
    {
        get => GetValue(CenterXProperty);
        set => SetValue(CenterXProperty, value);
    }

    public double Radius
    {
        get => GetValue(RadiusProperty);
        set => SetValue(RadiusProperty, value);
    }

    public double TopLeftX => CenterX - 0.5f;
    public double TopLeftY => CenterY - 0.5f;

    public double Diameter => Radius * 2;

    public event PropertyChangedEventHandler PropertyChanged;

    private Ellipse mainHandle;
    private bool isChangingRadius;
    private Point centerOnCapture;
    private Point positionOnCapture;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        mainHandle = e.NameScope.Get<Ellipse>("PART_MainHandle");

        AddHandle(mainHandle, (x, y) =>
        {
            if (isChangingRadius)
            {
                Point mousePos = new Point(x, y);
                Point centerPos = new Point(CenterX, CenterY);

                double distance = GetDistance(mousePos, centerPos);
                distance = Math.Clamp(distance, 0.05, 0.5f);

                Radius = distance;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Diameter)));
            }
            else
            {
                Point delta = new Point(x - positionOnCapture.X, y - positionOnCapture.Y);
                CenterX = centerOnCapture.X + delta.X;
                CenterY = centerOnCapture.Y + delta.Y;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TopLeftX)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TopLeftY)));
            }
        });
    }

    protected override void OnCapturingHandle(InputElement handle, Point normalizedPos)
    {
        if (handle == mainHandle)
        {
            double thickness = mainHandle.StrokeThickness / (mainHandle.Parent as Control).Bounds.Width;
            Point mousePos = new Point(normalizedPos.X, normalizedPos.Y);
            Point centerPos = new Point(CenterX, CenterY);
            centerOnCapture = centerPos;
            positionOnCapture = mousePos;

            double distance = GetDistance(mousePos, centerPos);
            isChangingRadius = distance >= Radius - thickness && distance <= Radius + thickness;
        }
    }

    private double GetDistance(Point a, Point b)
    {
        return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
    }
}
