using Avalonia;
using Avalonia.Controls.Primitives;

namespace ColorPicker;

public class LinearGradientPointPad : TemplatedControl
{
    public static readonly StyledProperty<Point> StartPointProperty =
        AvaloniaProperty.Register<LinearGradientPointPad, Point>(
            nameof(StartPoint));

    public static readonly StyledProperty<Point> EndPointProperty =
        AvaloniaProperty.Register<LinearGradientPointPad, Point>(
            nameof(EndPoint));

    public Point EndPoint
    {
        get => GetValue(EndPointProperty);
        set => SetValue(EndPointProperty, value);
    }

    public Point StartPoint
    {
        get => GetValue(StartPointProperty);
        set => SetValue(StartPointProperty, value);
    }


}
