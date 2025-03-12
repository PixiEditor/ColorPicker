using Avalonia;
using Avalonia.Controls;

namespace ColorPicker;

public class UniformPanel : Panel
{
    protected override Size MeasureOverride(Size availableSize)
    {
        double minSize = Math.Min(availableSize.Width, availableSize.Height);

        return new Size(minSize, minSize);
    }
}