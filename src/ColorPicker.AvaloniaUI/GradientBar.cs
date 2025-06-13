using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using ColorPicker.Models;
using ColorPicker.Utilities;
using GradientStop = ColorPicker.Models.GradientStop;

namespace ColorPicker;

[TemplatePart("PART_Bar", typeof(Border))]
[TemplatePart("PART_GradientStops", typeof(ItemsControl))]
[TemplatePart("PART_RemoveStopButton", typeof(Button))]
public class GradientBar : TemplatedControl, IGradientStorage, IColorStateStorage
{
    public static readonly StyledProperty<GradientState> GradientStateProperty =
        AvaloniaProperty.Register<GradientBar, GradientState>(
            nameof(GradientState));

    public static readonly StyledProperty<int> SelectedStopIndexProperty = AvaloniaProperty.Register<GradientBar, int>(
        nameof(SelectedStopIndex));

    public static readonly StyledProperty<Avalonia.Media.GradientStop> SelectedStopProperty =
        AvaloniaProperty.Register<GradientBar, Avalonia.Media.GradientStop>(
            nameof(SelectedStop));

    public static readonly StyledProperty<ColorState> SelectedStopStateProperty =
        AvaloniaProperty.Register<GradientBar, ColorState>(
            nameof(SelectedStopState));

    public static readonly StyledProperty<NotifyableColor> SelectedStopBindableProperty =
        AvaloniaProperty.Register<GradientBar, NotifyableColor>(
            nameof(SelectedStopBindable));

    public static readonly StyledProperty<double> SelectedStopOffsetProperty =
        AvaloniaProperty.Register<GradientBar, double>(
            nameof(SelectedStopOffset));

    public double SelectedStopOffset
    {
        get => GetValue(SelectedStopOffsetProperty);
        set => SetValue(SelectedStopOffsetProperty, value);
    }

    public NotifyableColor SelectedStopBindable
    {
        get => GetValue(SelectedStopBindableProperty);
        set => SetValue(SelectedStopBindableProperty, value);
    }

    public static readonly StyledProperty<GradientStops> GradientStopsProperty =
        AvaloniaProperty.Register<GradientBar, GradientStops>(
            nameof(GradientStops));

    public static readonly StyledProperty<ICommand> SelectColorStopCommandProperty =
        AvaloniaProperty.Register<GradientBar, ICommand>(
            nameof(SelectColorStopCommand));

    public ICommand SelectColorStopCommand
    {
        get => GetValue(SelectColorStopCommandProperty);
        set => SetValue(SelectColorStopCommandProperty, value);
    }

    public GradientStops GradientStops
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

    ColorState IColorStateStorage.ColorState
    {
        get => SelectedStopState;
        set => SelectedStopState = value;
    }

    private bool isUpdating;

    static GradientBar()
    {
        SelectedStopStateProperty.Changed.AddClassHandler<GradientBar, ColorState>(StopChanged);
        SelectedStopOffsetProperty.Changed.AddClassHandler<GradientBar, double>(StopOffsetChanged);
        SelectedStopIndexProperty.Changed.AddClassHandler<GradientBar, int>(IndexChanged);
        GradientStateProperty.Changed.AddClassHandler<GradientBar, GradientState>(GradientStateChanged);
    }

    private Border bar;
    private ItemsControl stops;
    private Button removeStopButton;

    private bool pauseStateUpdate;

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

        GradientStops = new GradientStops
        {
            new Avalonia.Media.GradientStop(ToColor(stop0), 0), new Avalonia.Media.GradientStop(ToColor(stop1), 1)
        };

        SelectedStopIndex = 0;
        SelectedStopState = stop0;
        SelectedStopOffset = 0;

        SelectColorStopCommand = new RelayCommand<Avalonia.Media.GradientStop>(stop =>
        {
            int foundIndex = GradientStops.IndexOf(stop);

            if (foundIndex != -1)
            {
                SelectedStopIndex = foundIndex;
                SelectedStopState = GradientState.Stops[foundIndex].ColorState;
                SelectedStopOffset = GradientStops[foundIndex].Offset;
                SelectedStop = GradientStops[foundIndex];
            }
        });

        SelectedStopBindable = new NotifyableColor(this);
    }

    public void AddStop(double offset)
    {
        ColorState colorOnOffset = GradientState.Evaluate(offset);
        GradientState newGradientState = GradientState.WithAddedStop(new GradientStop
        {
            ColorState = colorOnOffset, Offset = offset
        });

        UpdateInternalState(newGradientState);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        bar = e.NameScope.Find<Border>("PART_Bar");
        bar.PointerPressed += BarOnPointerPressed;
        bar.PointerMoved += BarMoved;

        stops = e.NameScope.Find<ItemsControl>("PART_GradientStops");

        removeStopButton = e.NameScope.Find<Button>("PART_RemoveStopButton");
        if (removeStopButton != null)
        {
            removeStopButton.Click += RemoveStopButtonOnClick;
        }
    }

    private void BarMoved(object sender, PointerEventArgs e)
    {
        if (e.Pointer.Captured != null && e.Pointer.Captured.Equals(bar))
        {
            if (e.GetCurrentPoint(bar).Properties.IsLeftButtonPressed)
            {
                double min = 0;
                double max = 1;

                if (SelectedStopIndex - 1 >= 0)
                {
                    min = GradientState.Stops[SelectedStopIndex - 1].Offset;
                }

                if (SelectedStopIndex + 1 < GradientState.Stops.Count)
                {
                    max = GradientState.Stops[SelectedStopIndex + 1].Offset;
                }

                double offset = GetNormalizedOffset(e);
                offset = Math.Clamp(offset, min, max);

                GradientState newGradientState = GradientState.WithUpdatedStop(SelectedStopIndex,
                    new GradientStop
                    {
                        ColorState = GradientState.Stops[SelectedStopIndex].ColorState, Offset = offset
                    });

                UpdateInternalState(newGradientState);
            }
        }
    }

    private void BarOnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        bool pressedOnBar = e.Source != null && e.Source.Equals(stops.ItemsPanelRoot);

        double offset = GetNormalizedOffset(e);

        if (pressedOnBar)
        {
            AddStop(offset);
        }

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

        if (pressedOnBar)
        {
            GradientState newGradientState = GradientState.WithUpdatedStop(SelectedStopIndex,
                new GradientStop { ColorState = GradientState.Stops[SelectedStopIndex].ColorState, Offset = offset });

            UpdateInternalState(newGradientState);
        }
    }

    private double GetNormalizedOffset(PointerEventArgs e)
    {
        return Math.Clamp(
            (e.GetPosition(bar).X - bar.Padding.Left) / (bar.Bounds.Width - (bar.Padding.Left + bar.Padding.Right)), 0,
            1);
    }

    private static Color ToColor(ColorState colorState)
    {
        return Color.FromArgb((byte)(colorState.A * 255), (byte)(colorState.RGB_R * 255),
            (byte)(colorState.RGB_G * 255), (byte)(colorState.RGB_B * 255));
    }

    private void UpdateInternalState(GradientState newGradientState)
    {
        isUpdating = true;
        GradientState = newGradientState;

        if (GradientState.Stops == null || GradientState.Stops.Count == 0)
        {
            GradientStops = new GradientStops();
            SelectedStopIndex = 0;
            return;
        }

        SelectedStopIndex = Math.Clamp(SelectedStopIndex, 0, GradientState.Stops.Count - 1);

        if (GradientStops == null)
        {
            GradientStops = new GradientStops();
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
        SelectedStopOffset = GradientStops[SelectedStopIndex].Offset;
        SelectedStop = GradientStops[SelectedStopIndex];
        isUpdating = false;
    }

    private void RemoveStopButtonOnClick(object sender, RoutedEventArgs e)
    {
        if (GradientState.Stops.Count > 2)
        {
            GradientState newGradientState = GradientState.WitRemovedStop(SelectedStopIndex);
            UpdateInternalState(newGradientState);
        }
    }

    private static void StopChanged(GradientBar sender, AvaloniaPropertyChangedEventArgs<ColorState> e)
    {
        if (sender.isUpdating)
        {
            return;
        }

        var newStop = new GradientStop
        {
            ColorState = e.NewValue.Value, Offset = sender.GradientState.Stops[sender.SelectedStopIndex].Offset
        };

        var updated = sender.GradientState.WithUpdatedStop(sender.SelectedStopIndex, newStop);
        sender.UpdateInternalState(updated);
    }

    private static void StopOffsetChanged(GradientBar sender, AvaloniaPropertyChangedEventArgs<double> e)
    {
        if (sender.isUpdating)
        {
            return;
        }

        var newStop = new GradientStop
        {
            ColorState = sender.GradientState.Stops[sender.SelectedStopIndex].ColorState, Offset = e.NewValue.Value
        };

        var updated = sender.GradientState.WithUpdatedStop(sender.SelectedStopIndex, newStop);
        sender.UpdateInternalState(updated);
    }

    private static void IndexChanged(GradientBar sender, AvaloniaPropertyChangedEventArgs<int> e)
    {
        if (sender.isUpdating) return;

        sender.SelectedStopState = sender.GradientState.Stops[e.NewValue.Value].ColorState;
        sender.SelectedStopOffset = sender.GradientStops[e.NewValue.Value].Offset;
        sender.SelectedStop = sender.GradientStops[e.NewValue.Value];
    }

    private static void GradientStateChanged(GradientBar sender, AvaloniaPropertyChangedEventArgs<GradientState> e)
    {
        if (sender.pauseStateUpdate)
        {
            return;
        }

        sender.pauseStateUpdate = true;
        sender.UpdateInternalState(e.NewValue.Value);
        sender.pauseStateUpdate = false;
    }
}
