using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;

namespace ColorPicker;

public class RecentBrush : TemplatedControl
{
    public static readonly StyledProperty<ICommand> PressCommandProperty = AvaloniaProperty.Register<RecentBrush, ICommand>("PressCommand");

    public static readonly StyledProperty<IBrush> BrushProperty = AvaloniaProperty.Register<RecentBrush, IBrush>(
        nameof(Brush));

    public IBrush Brush
    {
        get => GetValue(BrushProperty);
        set => SetValue(BrushProperty, value);
    }

    public ICommand PressCommand
    {
        get { return (ICommand)GetValue(PressCommandProperty); }
        set { SetValue(PressCommandProperty, value); }
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        PressCommand?.Execute(Brush);
    }
}
