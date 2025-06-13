using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace ColorPicker;

public class RecentBrushes : TemplatedControl
{
    public static readonly StyledProperty<ObservableCollection<IBrush>> BrushesProperty = AvaloniaProperty.Register<RecentBrushes, ObservableCollection<IBrush>>(
        nameof(Brushes));

    public static readonly StyledProperty<ICommand> SelectBrushCommandProperty = AvaloniaProperty.Register<RecentBrushes, ICommand>(
        nameof(SelectBrushCommand));

    public ICommand SelectBrushCommand
    {
        get => GetValue(SelectBrushCommandProperty);
        set => SetValue(SelectBrushCommandProperty, value);
    }

    public ObservableCollection<IBrush> Brushes
    {
        get => GetValue(BrushesProperty);
        set => SetValue(BrushesProperty, value);
    }
}
