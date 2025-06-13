using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace ColorPicker;

public abstract class GradientPad : TemplatedControl
{
    private Dictionary<InputElement, Action<double, double>> handles = new();

    public void AddHandle(InputElement handle, Action<double, double> moved)
    {
        handles.Add(handle, moved);

        handle.PointerPressed += Handle_PointerPressed;
        handle.PointerMoved += Handle_PointerMoved;
    }

    protected virtual void OnCapturingHandle(InputElement handle, Point normalizedPos)
    {
    }

    private void Handle_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is InputElement element)
        {
            var normalizedPoint = ToNormalizedPos(element, e.GetPosition(this));
            if (normalizedPoint != null)
            {
                OnCapturingHandle(element, normalizedPoint.Value);
                e.Pointer.Capture(element);
            }
        }
    }

    private void Handle_PointerMoved(object? sender, PointerEventArgs e)
    {
        if (sender is InputElement element && Equals(e.Pointer.Captured, element) &&
            handles.TryGetValue(element, out var moved))
        {
            var pos = e.GetPosition(this);

            var normalizedPos = ToNormalizedPos(element, pos);
            if (normalizedPos == null)
            {
                return;
            }

            moved(normalizedPos.Value.X, normalizedPos.Value.Y);
        }
    }

    protected Point? ToNormalizedPos(InputElement element, Point pos)
    {
        var parent = element.Parent as Control;
        if (parent == null)
        {
            return null;
        }

        double x = pos.X;
        double y = pos.Y;
        var bounds = parent.Bounds.Size;
        x = Math.Clamp(pos.X / bounds.Width, 0, 1);
        y = Math.Clamp(pos.Y / bounds.Height, 0, 1);

        return new Point(x, y);
    }
}
