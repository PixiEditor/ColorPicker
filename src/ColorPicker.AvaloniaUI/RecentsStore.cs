using System.Collections.ObjectModel;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace ColorPicker;

public class RecentsStore
{
    public int MaxRecentColors { get; set; } = 30;
    public static RecentsStore Global { get; } = new();

    public ObservableCollection<IBrush> RecentColors { get; } = new();
    public ObservableCollection<IBrush> RecentGradients { get; } = new();

    public void TryAddRecentColor(Color color)
    {
        int indexOfExisting =
            RecentColors.IndexOf(RecentColors.FirstOrDefault(x => x is ISolidColorBrush scb && scb.Color == color));
        if (indexOfExisting != -1)
        {
            RecentColors.Move(indexOfExisting, 0);
            return;
        }

        if (RecentColors.Count >= MaxRecentColors)
        {
            RecentColors.RemoveAt(RecentColors.Count - 1);
        }

        RecentColors.Insert(0, new ImmutableSolidColorBrush(color));
    }

    public void TryAddRecentGradient(IGradientBrush selectedGradient)
    {
        int indexOfExisting =
            RecentGradients.IndexOf(RecentGradients.FirstOrDefault(x =>
                x is IGradientBrush gb && GradientEquals(selectedGradient, gb)));
        if (indexOfExisting != -1)
        {
            RecentGradients.Move(indexOfExisting, 0);
            return;
        }

        if (RecentGradients.Count >= MaxRecentColors)
        {
            RecentGradients.RemoveAt(RecentGradients.Count - 1);
        }

        RecentGradients.Insert(0, selectedGradient.ToImmutable());
    }

    private static bool GradientEquals(IGradientBrush gradient1, IGradientBrush gradient2)
    {
        if (gradient1 is ILinearGradientBrush linearGradient1 && gradient2 is ILinearGradientBrush linearGradient2)
        {
            return StopsEquals(linearGradient1.GradientStops, linearGradient2.GradientStops)
                   && linearGradient1.StartPoint == linearGradient2.StartPoint
                   && linearGradient1.EndPoint == linearGradient2.EndPoint;
        }

        if (gradient1 is IRadialGradientBrush radialGradient1 && gradient2 is IRadialGradientBrush radialGradient2)
        {
            return StopsEquals(radialGradient1.GradientStops, radialGradient2.GradientStops)
                   && radialGradient1.Center == radialGradient2.Center
                   && radialGradient1.RadiusX == radialGradient2.RadiusX
                   && radialGradient1.RadiusY == radialGradient2.RadiusY;
        }

        if (gradient1 is IConicGradientBrush conicGradient1 && gradient2 is IConicGradientBrush conicGradient2)
        {
            return StopsEquals(conicGradient1.GradientStops, conicGradient2.GradientStops)
                   && conicGradient1.Center == conicGradient2.Center
                   && conicGradient1.Angle == conicGradient2.Angle;
        }

        return false;
    }

    private static bool StopsEquals(IReadOnlyList<IGradientStop> stops1, IReadOnlyList<IGradientStop> stops2)
    {
        if (stops1.Count != stops2.Count)
        {
            return false;
        }

        for (int i = 0; i < stops1.Count; i++)
        {
            if (stops1[i].Color != stops2[i].Color || Math.Abs(stops1[i].Offset - stops2[i].Offset) > 0.001f)
            {
                return false;
            }
        }

        return true;
    }
}
