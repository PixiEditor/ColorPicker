using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using ColorPicker.Models;
using ColorPicker.Utilities;
using GradientStop = ColorPicker.Models.GradientStop;

namespace ColorPicker;

[TemplatePart("PART_Bar", typeof(Border))]
[TemplatePart("PART_GradientStops", typeof(ItemsControl))]
public class GradientBar : TemplatedControl, IGradientStorage
{
    public static readonly StyledProperty<GradientState> GradientStateProperty =
        AvaloniaProperty.Register<GradientBar, GradientState>(
            nameof(GradientState));

    public static readonly StyledProperty<GradientBrush> BrushProperty =
        AvaloniaProperty.Register<GradientBar, GradientBrush>(
            nameof(Brush));

    public static readonly StyledProperty<int> SelectedStopIndexProperty = AvaloniaProperty.Register<GradientBar, int>(
        nameof(SelectedStopIndex));

    public static readonly StyledProperty<Avalonia.Media.GradientStop> SelectedStopProperty =
        AvaloniaProperty.Register<GradientBar, Avalonia.Media.GradientStop>(
            nameof(SelectedStop));

    public static readonly StyledProperty<ColorState> SelectedStopStateProperty =
        AvaloniaProperty.Register<GradientBar, ColorState>(
            nameof(SelectedStopState));

    public static readonly StyledProperty<ObservableCollection<Avalonia.Media.GradientStop>> GradientStopsProperty =
        AvaloniaProperty.Register<GradientBar, ObservableCollection<Avalonia.Media.GradientStop>>(
            nameof(GradientStops));

    public static readonly StyledProperty<ICommand> SelectColorStopCommandProperty =
        AvaloniaProperty.Register<GradientBar, ICommand>(
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

    public Avalonia.Media.GradientStop SelectedStop
    {
        get => GetValue(SelectedStopProperty);
        set => SetValue(SelectedStopProperty, value);
    }

    static GradientBar()
    {
        SelectedStopStateProperty.Changed.AddClassHandler<GradientBar, ColorState>(StopChanged);
        SelectedStopIndexProperty.Changed.AddClassHandler<GradientBar, int>(IndexChanged);
        GradientStateProperty.Changed.AddClassHandler<GradientBar, GradientState>(GradientStateChanged);
    }

    private Border bar;
    private ItemsControl stops;

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
                SelectedStopState = GradientState.Stops[foundIndex].ColorState;
                SelectedStop = GradientStops[foundIndex];
            }
        });
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        bar = e.NameScope.Find<Border>("PART_Bar");
        bar.PointerPressed += BarOnPointerPressed;
        bar.PointerMoved += BarMoved;

        stops = e.NameScope.Find<ItemsControl>("PART_GradientStops");
    }

    private void BarMoved(object sender, PointerEventArgs e)
    {
        if (e.Pointer.Captured != null && e.Pointer.Captured.Equals(bar))
        {
            if (e.GetCurrentPoint(bar).Properties.IsLeftButtonPressed)
            {
                double offset = GetNormalizedOffset(e);

                GradientState newGradientState = GradientState.WithUpdatedStop(SelectedStopIndex,
                    new GradientStop
                        { ColorState = GradientState.Stops[SelectedStopIndex].ColorState, Offset = offset });

                UpdateInternalState(newGradientState);
            }
        }
    }

    private void BarOnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        double normalizedClosestOffset = GetNormalizedOffset(e);

        int closestStopIndex = 0;
        double closestDistance = double.MaxValue;

        for (int i = 0; i < GradientState.Stops.Count; i++)
        {
            double distance = Math.Abs(GradientState.Stops[i].Offset - normalizedClosestOffset);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestStopIndex = i;
            }
        }

        SelectedStopIndex = closestStopIndex;

        e.Pointer.Capture(bar);

        if (e.Source != null && e.Source.Equals(stops.ItemsPanelRoot))
        {
            double offset = GetNormalizedOffset(e);

            GradientState newGradientState = GradientState.WithUpdatedStop(SelectedStopIndex,
                new GradientStop
                    { ColorState = GradientState.Stops[SelectedStopIndex].ColorState, Offset = offset });

            UpdateInternalState(newGradientState);
        }
    }

    private double GetNormalizedOffset(PointerEventArgs e)
    {
        return Math.Clamp(
            (e.GetPosition(bar).X - bar.Padding.Left) / (bar.Bounds.Width - (bar.Padding.Left + bar.Padding.Right)), 0,
            1);
    }

    private void GenerateBrush()
    {
        GradientStops stops = new GradientStops();
        foreach (var stop in GradientStops)
        {
            stops.Add(stop);
        }

        Brush = new LinearGradientBrush()
        {
            GradientStops = stops
        };
    }

    private static Color ToColor(ColorState colorState)
    {
        return Color.FromArgb((byte)(colorState.A * 255), (byte)(colorState.RGB_R * 255),
            (byte)(colorState.RGB_G * 255), (byte)(colorState.RGB_B * 255));
    }

    private void UpdateInternalState(GradientState newGradientState)
    {
        GradientState = newGradientState;

        if (GradientStops == null)
        {
            GradientStops = new ObservableCollection<Avalonia.Media.GradientStop>();
        }
        else
        {
            GradientStops.Clear();
        }

        foreach (var stop in GradientState.Stops)
        {
            GradientStops.Add(new Avalonia.Media.GradientStop(ToColor(stop.ColorState), stop.Offset));
        }

        SelectedStopState = GradientState.Stops[SelectedStopIndex].ColorState;
        SelectedStop = GradientStops[SelectedStopIndex];

        GenerateBrush();
    }

    private static void StopChanged(GradientBar sender, AvaloniaPropertyChangedEventArgs<ColorState> e)
    {
        var newStop = new GradientStop
        {
            ColorState = e.NewValue.Value, Offset = sender.GradientState.Stops[sender.SelectedStopIndex].Offset
        };

        sender.UpdateInternalState(sender.GradientState.WithUpdatedStop(sender.SelectedStopIndex, newStop));
    }

    private static void IndexChanged(GradientBar sender, AvaloniaPropertyChangedEventArgs<int> e)
    {
        sender.SelectedStopState = sender.GradientState.Stops[e.NewValue.Value].ColorState;
        sender.SelectedStop = sender.GradientStops[e.NewValue.Value];
    }

    private static void GradientStateChanged(GradientBar sender, AvaloniaPropertyChangedEventArgs<GradientState> e)
    {
        sender.UpdateInternalState(e.NewValue.Value);
    }
}