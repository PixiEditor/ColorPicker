using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using ColorPicker.Models;
using Path = Avalonia.Controls.Shapes.Path;

namespace ColorPicker;

[TemplatePart(Name = "PART_Handle", Type = typeof(Path))]
public class HueSlider : TemplatedControl
{
    public static readonly StyledProperty<double> SmallChangeProperty = AvaloniaProperty.Register<HueSlider, double>(
        nameof(SmallChange), 1);

    public static readonly StyledProperty<double> ValueProperty = AvaloniaProperty.Register<HueSlider, double>(
        nameof(Value));

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var handlerPart = e.NameScope.Find<Path>("PART_Handle");
        handlerPart.AddHandler(PointerPressedEvent, OnMouseDown, RoutingStrategies.Tunnel);
        handlerPart.AddHandler(PointerReleasedEvent, OnMouseUp, RoutingStrategies.Tunnel);
        handlerPart.AddHandler(PointerMovedEvent, OnMouseMove, RoutingStrategies.Tunnel);
        handlerPart.AddHandler(PointerWheelChangedEvent, OnPreviewMouseWheel, RoutingStrategies.Tunnel);
    }

    public double SmallChange
    {
        get => GetValue(SmallChangeProperty);
        set => SetValue(SmallChangeProperty, value);
    }

    public double Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    private void OnMouseDown(object sender, PointerPressedEventArgs e)
    {
        var circle = (Path)sender;
        e.Pointer.Capture(circle);
        var mousePos = e.GetPosition(circle);
        UpdateValue(mousePos, circle.Bounds.Width, circle.Bounds.Height);
        
        e.Handled = true;
    }

    private void OnMouseUp(object sender, PointerReleasedEventArgs e)
    {
        e.Pointer.Capture(null);
    }

    private void OnMouseMove(object sender, PointerEventArgs e)
    {
        if (e.Pointer.Captured == null)
            return;
        var circle = (Path)sender;
        var mousePos = e.GetPosition(circle);
        UpdateValue(mousePos, circle.Bounds.Width, circle.Bounds.Height);
        
        e.Handled = true;
    }

    private void UpdateValue(Point mousePos, double width, double height)
    {
        var x = mousePos.X / (width * 2);
        var y = mousePos.Y / (height * 2);

        var length = Math.Sqrt(x * x + y * y);
        if (length == 0)
            return;
        var angle = Math.Acos(x / length);
        if (y < 0)
            angle = -angle;
        angle = angle * 360 / (Math.PI * 2) + 180;
        Value = MathHelper.Clamp(angle, 0, 360);
    }

    private void OnPreviewMouseWheel(object sender, PointerWheelEventArgs args)
    {
        Value = MathHelper.Mod(Value + SmallChange * args.Delta.Y, 360);
        args.Handled = true;
    }
}