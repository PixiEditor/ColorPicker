using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using ColorPicker.Models;
using ColorPicker.Utilities;
using GradientStop = ColorPicker.Models.GradientStop;

namespace ColorPicker;

public class GradientBar : TemplatedControl, IGradientStorage
{
    public static readonly StyledProperty<GradientState> GradientStateProperty = AvaloniaProperty.Register<GradientBar, GradientState>(
        nameof(GradientState));

    public static readonly StyledProperty<GradientBrush> BrushProperty = AvaloniaProperty.Register<GradientBar, GradientBrush>(
        nameof(Brush));

    public static readonly StyledProperty<int> SelectedStopIndexProperty = AvaloniaProperty.Register<GradientBar, int>(
        nameof(SelectedStopIndex));

    public static readonly StyledProperty<ColorState> SelectedStopStateProperty = AvaloniaProperty.Register<GradientBar, ColorState>(
        nameof(SelectedStopState));

    public static readonly StyledProperty<ObservableCollection<Avalonia.Media.GradientStop>> GradientStopsProperty = AvaloniaProperty.Register<GradientBar, ObservableCollection<Avalonia.Media.GradientStop>>(
        nameof(GradientStops));

    public static readonly StyledProperty<ICommand> SelectColorStopCommandProperty = AvaloniaProperty.Register<GradientBar, ICommand>(
        nameof(SelectColorStopCommand));

    public ICommand SelectColorStopCommand
    {
        get => GetValue(SelectColorStopCommandProperty);
        set => SetValue(SelectColorStopCommandProperty, value);
    }

    public ObservableCollection<Avalonia.Media.GradientStop> GradientStops
    {
        get => GetValue(GradientStopsProperty);
        set => SetValue(GradientStopsProperty, value);
    }

    public ColorState SelectedStopState
    {
        get => GetValue(SelectedStopStateProperty);
        set => SetValue(SelectedStopStateProperty, value);
    }

    public int SelectedStopIndex
    {
        get => GetValue(SelectedStopIndexProperty);
        set => SetValue(SelectedStopIndexProperty, value);
    }

    public GradientBrush Brush
    {
        get => GetValue(BrushProperty);
        set => SetValue(BrushProperty, value);
    }

    public GradientState GradientState
    {
        get => GetValue(GradientStateProperty);
        set => SetValue(GradientStateProperty, value);
    }

    static GradientBar()
    {
        SelectedStopStateProperty.Changed.AddClassHandler<GradientBar, ColorState>(StopChanged);
    }

    public GradientBar()
    {
        ColorState stop0 = new();
        stop0.SetARGB(1, 0, 0, 0);
        ColorState stop1 = new();
        stop1.SetARGB(1, 1, 1, 1);

        GradientState = new GradientState(new List<GradientStop>
        {
            new GradientStop() { ColorState = stop0, Offset = 0 },
            new GradientStop() { ColorState = stop1, Offset = 1 }
        });

        GradientStops = new ObservableCollection<Avalonia.Media.GradientStop>(new[]
        {
            new Avalonia.Media.GradientStop(ToColor(stop0), 0),
            new Avalonia.Media.GradientStop(ToColor(stop1), 1)
        });

        GenerateBrush();

        SelectedStopIndex = 0;
        SelectedStopState = stop0;

        SelectColorStopCommand = new RelayCommand<Avalonia.Media.GradientStop>(stop =>
        {
            int foundIndex = GradientStops.IndexOf(stop);
            if (foundIndex != -1)
            {
                SelectedStopIndex = foundIndex;
                SelectedStopState = GradientState.GradientStops[SelectedStopIndex].ColorState;
            }
        });
    }

    private void GenerateBrush()
    {
        GradientStops stops = new GradientStops();
        foreach (var stop in GradientState.GradientStops)
        {
            stops.Add(new Avalonia.Media.GradientStop(ToColor(stop.ColorState), stop.Offset));
        }

        Brush = new LinearGradientBrush()
        {
            GradientStops = stops
        };
    }

    private static Color ToColor(ColorState colorState)
    {
        return Color.FromArgb((byte)(colorState.A * 255), (byte)(colorState.RGB_R * 255), (byte)(colorState.RGB_G * 255), (byte)(colorState.RGB_B * 255));
    }

    private static void StopChanged(GradientBar sender, AvaloniaPropertyChangedEventArgs<ColorState> e)
    {
        sender.GradientState.GradientStops[sender.SelectedStopIndex] = new GradientStop()
        {
            ColorState = e.NewValue.Value, Offset = sender.GradientState.GradientStops[sender.SelectedStopIndex].Offset
        };

        sender.GenerateBrush();
    }
}