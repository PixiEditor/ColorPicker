using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;

namespace ColorPicker;

[TemplatePart("PART_StartHandle", typeof(Control))]
[TemplatePart("PART_EndHandle", typeof(Control))]
public class LinearGradientPointPad : TemplatedControl
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

    public static readonly StyledProperty<GradientStop> StartGradientStopProperty = AvaloniaProperty.Register<LinearGradientPointPad, GradientStop>(
        nameof(StartGradientStop));

    public static readonly StyledProperty<GradientStop> EndGradientStopProperty = AvaloniaProperty.Register<LinearGradientPointPad, GradientStop>(
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

        startHandle.PointerPressed += StartHandlePressed;
        startHandle.PointerMoved += StartHandleOnPointerMoved;
        endHandle.PointerPressed += EndHandlePressed;
        endHandle.PointerMoved += EndHandleOnPointerMoved;
    }

    private void StartHandlePressed(object sender, PointerPressedEventArgs e)
    {
        e.Pointer.Capture(startHandle);
    }

    private void EndHandlePressed(object sender, PointerPressedEventArgs e)
    {
        e.Pointer.Capture(endHandle);
    }

    private void StartHandleOnPointerMoved(object sender, PointerEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            var pos = e.GetPosition(this);
            var parent = startHandle.Parent as Control;
            var bounds = parent.Bounds.Size;
            StartPointX = Math.Clamp(pos.X / bounds.Width, 0, 1);
            StartPointY = Math.Clamp(pos.Y / bounds.Height, 0, 1);
        }
    }

    private void EndHandleOnPointerMoved(object sender, PointerEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            var parent = endHandle.Parent as Control;
            var bounds = parent.Bounds.Size;
            var pos = e.GetPosition(this);
            EndPointX = Math.Clamp(pos.X / bounds.Width, 0, 1);
            EndPointY = Math.Clamp(pos.Y / bounds.Height, 0, 1);
        }
    }
}
