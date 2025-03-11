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

    public Cursor CurrentAngleCursor { get; private set; }

    private Cursor westEastCursor = new Cursor(StandardCursorType.SizeWestEast);
    private Cursor northSouthCursor = new Cursor(StandardCursorType.SizeNorthSouth);

    public event PropertyChangedEventHandler PropertyChanged;

    private Ellipse mainHandle;
    private bool isChangingRadius;
    private Point centerOnCapture;
    private Point positionOnCapture;

    static RadialGradientPointPad()
    {
        AffectsRender<RadialGradientPointPad>(CenterXProperty, CenterYProperty, RadiusProperty);
        CenterXProperty.Changed.Subscribe(CenterChanged);
        CenterYProperty.Changed.Subscribe(CenterChanged);
        RadiusProperty.Changed.Subscribe(RadiusChanged);
    }

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
            }
            else
            {
                Point delta = new Point(x - positionOnCapture.X, y - positionOnCapture.Y);
                CenterX = centerOnCapture.X + delta.X;
                CenterY = centerOnCapture.Y + delta.Y;
            }
        });
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);
        Point? normalizedPos = ToNormalizedPos(mainHandle, e.GetPosition(this));
        if (normalizedPos != null)
        {
            if (!IsOverBorder(normalizedPos.Value))
            {
                CurrentAngleCursor = null;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentAngleCursor)));
                return;
            }

            double angle = Math.Atan2(normalizedPos.Value.Y - CenterY, normalizedPos.Value.X - CenterX) * 180 / Math.PI;

            if (angle < 0)
            {
                angle += 360;
            }

            Cursor cursor = angle is > 45 and < 135 or > 225 and < 315 ? northSouthCursor : westEastCursor;
            CurrentAngleCursor = cursor;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentAngleCursor)));
        }
    }

    protected override void OnCapturingHandle(InputElement handle, Point normalizedPos)
    {
        if (handle == mainHandle)
        {
            Point centerPos = new Point(CenterX, CenterY);
            centerOnCapture = centerPos;
            positionOnCapture = normalizedPos;
            isChangingRadius = IsOverBorder(normalizedPos);
        }
    }

    private bool IsOverBorder(Point normalizedPos)
    {
        double thickness = mainHandle.StrokeThickness / (mainHandle.Parent as Control).Bounds.Width;
        Point centerPos = new Point(CenterX, CenterY);

        double distance = GetDistance(normalizedPos, centerPos);
        return distance >= Radius - thickness && distance <= Radius + thickness;
    }

    private double GetDistance(Point a, Point b)
    {
        return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
    }

    private static void CenterChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Sender is RadialGradientPointPad pad)
        {
            pad.PropertyChanged?.Invoke(pad, new PropertyChangedEventArgs(nameof(TopLeftX)));
            pad.PropertyChanged?.Invoke(pad, new PropertyChangedEventArgs(nameof(TopLeftY)));
        }
    }

    private static void RadiusChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Sender is RadialGradientPointPad pad)
        {
            pad.PropertyChanged?.Invoke(pad, new PropertyChangedEventArgs(nameof(Diameter)));
        }
    }
}
