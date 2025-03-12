using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;

namespace ColorPicker;

[TemplatePart("PART_StartHandle", typeof(Control))]
[TemplatePart("PART_EndHandle", typeof(Control))]
public class LinearGradientPointPad : GradientPad
{
    public static readonly StyledProperty<double> StartPointXProperty =
        AvaloniaProperty.Register<LinearGradientPointPad, double>(
            nameof(StartPointX));

    public static readonly StyledProperty<double> StartPointYProperty =
        AvaloniaProperty.Register<LinearGradientPointPad, double>(
            nameof(StartPointY));

    public static readonly StyledProperty<double> EndPointXProperty =
        AvaloniaProperty.Register<LinearGradientPointPad, double>(
            nameof(EndPointX));

    public static readonly StyledProperty<double> EndPointYProperty =
        AvaloniaProperty.Register<LinearGradientPointPad, double>(
            nameof(EndPointY));

    public static readonly StyledProperty<GradientStop> StartGradientStopProperty =
        AvaloniaProperty.Register<LinearGradientPointPad, GradientStop>(
            nameof(StartGradientStop));

    public static readonly StyledProperty<GradientStop> EndGradientStopProperty =
        AvaloniaProperty.Register<LinearGradientPointPad, GradientStop>(
            nameof(EndGradientStop));

    public GradientStop EndGradientStop
    {
        get => GetValue(EndGradientStopProperty);
        set => SetValue(EndGradientStopProperty, value);
    }

    public GradientStop StartGradientStop
    {
        get => GetValue(StartGradientStopProperty);
        set => SetValue(StartGradientStopProperty, value);
    }

    public double EndPointY
    {
        get => GetValue(EndPointYProperty);
        set => SetValue(EndPointYProperty, value);
    }

    public double EndPointX
    {
        get => GetValue(EndPointXProperty);
        set => SetValue(EndPointXProperty, value);
    }

    public double StartPointY
    {
        get => GetValue(StartPointYProperty);
        set => SetValue(StartPointYProperty, value);
    }

    public double StartPointX
    {
        get => GetValue(StartPointXProperty);
        set => SetValue(StartPointXProperty, value);
    }


    private Control? startHandle;
    private Control? endHandle;

    static LinearGradientPointPad()
    {
        AffectsRender<LinearGradientPointPad>(StartPointXProperty, StartPointYProperty, EndPointXProperty,
            EndPointYProperty);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        startHandle = e.NameScope.Find<Control>("PART_StartHandle");
        endHandle = e.NameScope.Find<Control>("PART_EndHandle");

        AddHandle(startHandle, (x, y) =>
        {
            StartPointX = x;
            StartPointY = y;
        });

        AddHandle(endHandle, (x, y) =>
        {
            EndPointX = x;
            EndPointY = y;
        });
    }
}
