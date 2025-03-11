using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace ColorPicker;

[TemplatePart("PART_CenterHandle", typeof(Control))]
[TemplatePart("PART_AngleHandle", typeof(Control))]
public class ConicGradientPad : GradientPad, INotifyPropertyChanged
{
    public static readonly StyledProperty<double> CenterXProperty = AvaloniaProperty.Register<ConicGradientPad, double>(
        nameof(CenterX));

    public static readonly StyledProperty<double> CenterYProperty = AvaloniaProperty.Register<ConicGradientPad, double>(
        nameof(CenterY));

    public static readonly StyledProperty<double> AngleProperty = AvaloniaProperty.Register<ConicGradientPad, double>(
        nameof(Angle));

    public double Angle
    {
        get => GetValue(AngleProperty);
        set => SetValue(AngleProperty, value);
    }

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

    public double EndPointX => CenterX + Math.Cos((Angle - 90) * Math.PI / 180);
    public double EndPointY => CenterY + Math.Sin((Angle - 90) * Math.PI / 180);

    private Cursor westEastCursor = new Cursor(StandardCursorType.SizeWestEast);
    private Cursor northSouthCursor = new Cursor(StandardCursorType.SizeNorthSouth);

    public Cursor CurrentAngleCursor => Angle is > 45 and < 135 || Angle is > 225 and < 315 ? northSouthCursor : westEastCursor;

    public event PropertyChangedEventHandler PropertyChanged;

    private Control centerHandle;
    private Control angleHandle;

    static ConicGradientPad()
    {
        AngleProperty.Changed.Subscribe(UpdateEnd);
        CenterXProperty.Changed.Subscribe(UpdateEnd);
        CenterYProperty.Changed.Subscribe(UpdateEnd);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        centerHandle = e.NameScope.Get<Control>("PART_CenterHandle");
        angleHandle = e.NameScope.Get<Control>("PART_AngleHandle");

        AddHandle(centerHandle, (x, y) =>
        {
            CenterX = x;
            CenterY = y;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EndPointX)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EndPointY)));
        });

        AddHandle(angleHandle, (x, y) =>
        {
            var angle = Math.Atan2(y - CenterY, x - CenterX) * 180 / Math.PI + 90;
            if (angle < 0)
            {
                angle += 360;
            }

            Angle = angle;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EndPointX)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EndPointY)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentAngleCursor)));
        });
    }

    private static void UpdateEnd(AvaloniaPropertyChangedEventArgs<double> e)
    {
        if (e.Sender is ConicGradientPad pad)
        {
            pad.PropertyChanged?.Invoke(pad, new PropertyChangedEventArgs(nameof(EndPointX)));
            pad.PropertyChanged?.Invoke(pad, new PropertyChangedEventArgs(nameof(EndPointY)));
            pad.PropertyChanged?.Invoke(pad, new PropertyChangedEventArgs(nameof(CurrentAngleCursor)));
        }
    }
}
