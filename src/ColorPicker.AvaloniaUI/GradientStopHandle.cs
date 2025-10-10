using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using ColorPicker.UIExtensions;

namespace ColorPicker;

[PseudoClasses(":active")]
public class GradientStopHandle : TemplatedControl
{
    public static readonly StyledProperty<GradientStop> GradientStopProperty = AvaloniaProperty.Register<GradientStopHandle, GradientStop>(
        nameof(GradientStop));

    public static readonly StyledProperty<bool> IsActiveProperty = AvaloniaProperty.Register<GradientStopHandle, bool>(
        nameof(IsActive));

    public bool IsActive
    {
        get => GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }

    public GradientStop GradientStop
    {
        get => GetValue(GradientStopProperty);
        set => SetValue(GradientStopProperty, value);
    }

    static GradientStopHandle()
    {
        IsActiveProperty.Changed.Subscribe(ActiveChanged);
    }

    private static void ActiveChanged(AvaloniaPropertyChangedEventArgs<bool> e)
    {
        if (e.Sender is GradientStopHandle handle)
        {
            handle.PseudoClasses.Set(":active", e.NewValue.Value);
        }
    }
}
