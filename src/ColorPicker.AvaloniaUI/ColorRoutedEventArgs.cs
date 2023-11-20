using Avalonia.Interactivity;
using Avalonia.Media;

namespace ColorPicker;

public class ColorRoutedEventArgs : RoutedEventArgs
{
    public ColorRoutedEventArgs(RoutedEvent routedEvent, Color color) : base(routedEvent)
    {
        Color = color;
    }

    public Color Color { get; private set; }
}