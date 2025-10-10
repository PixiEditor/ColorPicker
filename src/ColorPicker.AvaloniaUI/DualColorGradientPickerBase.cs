using Avalonia;
using Avalonia.Media;
using ColorPicker.Models;
using ColorPicker.UIExtensions;
using GradientStop = ColorPicker.Models.GradientStop;
using Matrix = ColorPicker.Models.Matrix;

namespace ColorPicker;

public class DualColorGradientPickerBase : DualPickerControlBase, IGradientStorage
{
    public static readonly StyledProperty<bool> EnableGradientsTabProperty =
        AvaloniaProperty.Register<DualColorGradientPickerBase, bool>(
            nameof(EnableGradientsTab), true);

    public static readonly StyledProperty<GradientState> GradientStateProperty =
        AvaloniaProperty.Register<DualColorGradientPickerBase, GradientState>(
            nameof(GradientState));

    public static readonly StyledProperty<GradientBrush> GradientBrushProperty =
        AvaloniaProperty.Register<DualColorGradientPickerBase, GradientBrush>(
            nameof(GradientBrush));

    public static readonly StyledProperty<int> SelectedTabIndexProperty =
        AvaloniaProperty.Register<DualColorGradientPickerBase, int>(
            nameof(SelectedTabIndex));

    public static readonly StyledProperty<GradientType> GradientTypeProperty =
        AvaloniaProperty.Register<DualColorGradientPickerBase, GradientType>(
            nameof(GradientType));

    public static readonly StyledProperty<NotifyableGradient> NotifyableGradientProperty =
        AvaloniaProperty.Register<DualColorGradientPickerBase, NotifyableGradient>(
            nameof(NotifyableGradient));

    public NotifyableGradient NotifyableGradient
    {
        get => GetValue(NotifyableGradientProperty);
        set => SetValue(NotifyableGradientProperty, value);
    }

    public GradientType GradientType
    {
        get => GetValue(GradientTypeProperty);
        set => SetValue(GradientTypeProperty, value);
    }

    public int SelectedTabIndex
    {
        get => GetValue(SelectedTabIndexProperty);
        set => SetValue(SelectedTabIndexProperty, value);
    }

    public GradientBrush GradientBrush
    {
        get => GetValue(GradientBrushProperty);
        set => SetValue(GradientBrushProperty, value);
    }

    public GradientState GradientState
    {
        get => GetValue(GradientStateProperty);
        set => SetValue(GradientStateProperty, value);
    }

    public bool EnableGradientsTab
    {
        get => GetValue(EnableGradientsTabProperty);
        set => SetValue(EnableGradientsTabProperty, value);
    }

    private bool isUpdating;

    static DualColorGradientPickerBase()
    {
        GradientBrushProperty.Changed.Subscribe(OnGradientChanged);
        SelectedTabIndexProperty.Changed.Subscribe(SelectedTabChanged);
        GradientTypeProperty.Changed.Subscribe(GradientTypeChanged);
        GradientStateProperty.Changed.Subscribe(GradientStateChanged);
    }

    public DualColorGradientPickerBase()
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

        GradientBrush = new LinearGradientBrush
        {
            GradientStops = new GradientStops
            {
                new Avalonia.Media.GradientStop(Colors.Black, 0),
                new Avalonia.Media.GradientStop(Colors.White, 1)
            }
        };

        NotifyableGradient = new NotifyableGradient(this);
    }

    protected override void UpdateFromBrush(IBrush brush)
    {
        isUpdating = true;
        if (brush is GradientBrush gradientBrush)
        {
            GradientType = gradientBrush switch
            {
                ILinearGradientBrush => GradientType.Linear,
                IRadialGradientBrush => GradientType.Radial,
                IConicGradientBrush => GradientType.Conic,
                _ => throw new ArgumentOutOfRangeException()
            };

            var oldState = GradientState;
            GradientState = StateFromBrush(gradientBrush);
            NotifyableGradient?.UpdateEverything(oldState);
            GradientBrush = gradientBrush;
            SelectedTabIndex = EnableGradientsTab ? 1 : 0;
        }
        else
        {
            base.UpdateFromBrush(brush);
            SelectedTabIndex = 0;
        }

        isUpdating = false;
    }

    protected override void UpdateSelectedBrush()
    {
        if (SelectedTabIndex == 1)
        {
            SelectedBrush = GradientBrush;
        }
        else
        {
            base.UpdateSelectedBrush();
        }
    }

    private GradientState StateFromBrush(IGradientBrush brush)
    {
        List<GradientStop> stops = new List<GradientStop>();
        foreach (var stop in brush.GradientStops)
        {
            ColorState colorState = new ColorState();
            colorState.SetARGB(stop.Color.A / 255d, stop.Color.R / 255d, stop.Color.G / 255d, stop.Color.B / 255d);
            stops.Add(new GradientStop { ColorState = colorState, Offset = stop.Offset });
        }

        return StateFromBrush(stops, brush, GradientState);
    }

    private GradientState StateFromBrush(List<GradientStop> stops, IGradientBrush brush, GradientState oldState)
    {
        if (brush is ILinearGradientBrush linearBrush)
        {
            return new GradientState(stops)
            {
                LinearStartPointX = linearBrush.StartPoint.Point.X,
                LinearStartPointY = linearBrush.StartPoint.Point.Y,
                LinearEndPointX = linearBrush.EndPoint.Point.X,
                LinearEndPointY = linearBrush.EndPoint.Point.Y,
                ConicAngle = oldState.ConicAngle,
                ConicCenterX = oldState.ConicCenterX,
                ConicCenterY = oldState.ConicCenterY,
                RadialCenterX = oldState.RadialCenterX,
                RadialCenterY = oldState.RadialCenterY,
                RadialRadius = oldState.RadialRadius,
                AbsoluteUnits = linearBrush.StartPoint.Unit == RelativeUnit.Absolute ||
                                linearBrush.EndPoint.Unit == RelativeUnit.Absolute,
                Transform = linearBrush.Transform != null ? ToMatrix(linearBrush.Transform.Value) : Matrix.Identity
            };
        }

        if (brush is IRadialGradientBrush radialBrush)
        {
            return new GradientState(stops)
            {
                RadialCenterX = radialBrush.Center.Point.X,
                RadialCenterY = radialBrush.Center.Point.Y,
                RadialRadius = radialBrush.RadiusX.Scalar,
                ConicAngle = oldState.ConicAngle,
                ConicCenterX = oldState.ConicCenterX,
                ConicCenterY = oldState.ConicCenterY,
                LinearStartPointX = oldState.LinearStartPointX,
                LinearStartPointY = oldState.LinearStartPointY,
                LinearEndPointX = oldState.LinearEndPointX,
                LinearEndPointY = oldState.LinearEndPointY,
                AbsoluteUnits = radialBrush.Center.Unit == RelativeUnit.Absolute ||
                                radialBrush.RadiusX.Unit == RelativeUnit.Absolute,
                Transform = radialBrush.Transform != null ? ToMatrix(radialBrush.Transform.Value) : Matrix.Identity
            };
        }

        if (brush is IConicGradientBrush conicBrush)
        {
            return new GradientState(stops)
            {
                ConicCenterX = conicBrush.Center.Point.X,
                ConicCenterY = conicBrush.Center.Point.Y,
                ConicAngle = conicBrush.Angle,
                RadialCenterX = oldState.RadialCenterX,
                RadialCenterY = oldState.RadialCenterY,
                RadialRadius = oldState.RadialRadius,
                LinearStartPointX = oldState.LinearStartPointX,
                LinearStartPointY = oldState.LinearStartPointY,
                LinearEndPointX = oldState.LinearEndPointX,
                LinearEndPointY = oldState.LinearEndPointY,
                AbsoluteUnits = conicBrush.Center.Unit == RelativeUnit.Absolute,
                Transform = conicBrush.Transform != null ? ToMatrix(conicBrush.Transform.Value) : Matrix.Identity
            };
        }

        return new GradientState(stops);
    }

    private void UpdateGradientType()
    {
        var stops = GradientBrush?.GradientStops ?? new GradientStops();

        UpdateGradientBrush(stops, RelativeUnit.Relative);
    }

    private void UpdateGradientBrushFromState()
    {
        if (GradientState.Stops == null || GradientState.Stops.Count == 0)
        {
            GradientBrush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
                {
                    new Avalonia.Media.GradientStop(Colors.Black, 0),
                    new Avalonia.Media.GradientStop(Colors.White, 1)
                }
            };
            return;
        }

        GradientStops stops = new GradientStops();
        foreach (var stop in GradientState.Stops)
        {
            stops.Add(new Avalonia.Media.GradientStop(
                new Color(
                    (byte)(stop.ColorState.A * 255d),
                    (byte)(stop.ColorState.RGB_R * 255d),
                    (byte)(stop.ColorState.RGB_G * 255d),
                    (byte)(stop.ColorState.RGB_B * 255d)), stop.Offset));
        }

        UpdateGradientBrush(stops,
            GradientState.AbsoluteUnits ? RelativeUnit.Absolute : RelativeUnit.Relative);
    }

    private void UpdateGradientBrush(GradientStops stops, RelativeUnit unit)
    {
        GradientBrush = GradientType switch
        {
            GradientType.Linear => new LinearGradientBrush
            {
                GradientStops = stops,
                StartPoint =
                    new RelativePoint(GradientState.LinearStartPointX, GradientState.LinearStartPointY,
                        unit),
                EndPoint = new RelativePoint(GradientState.LinearEndPointX, GradientState.LinearEndPointY,
                    unit),
                Transform = new MatrixTransform(ToAvMatrix(GradientState.Transform))
            },
            GradientType.Radial => new RadialGradientBrush
            {
                GradientStops = stops,
                Center =
                    new RelativePoint(GradientState.RadialCenterX, GradientState.RadialCenterY, unit),
                RadiusX = new RelativeScalar(GradientState.RadialRadius, RelativeUnit.Relative),
                RadiusY = new RelativeScalar(GradientState.RadialRadius, RelativeUnit.Relative),
                Transform = new MatrixTransform(ToAvMatrix(GradientState.Transform))
            },
            GradientType.Conic => new ConicGradientBrush
            {
                GradientStops = stops,
                Center = new RelativePoint(GradientState.ConicCenterX, GradientState.ConicCenterY,
                    unit),
                Angle = GradientState.ConicAngle,
                Transform = new MatrixTransform(ToAvMatrix(GradientState.Transform))
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static void OnGradientChanged(AvaloniaPropertyChangedEventArgs<GradientBrush> args)
    {
        if (args.Sender is DualColorGradientPickerBase picker)
        {
            if (picker.isUpdating) return;

            picker.UpdateSelectedBrush();
        }
    }

    private static void SelectedTabChanged(AvaloniaPropertyChangedEventArgs<int> args)
    {
        if (args.Sender is DualColorGradientPickerBase picker)
        {
            if (picker.isUpdating || picker.SelectedTabIndex == args.NewValue) return;

            picker.UpdateSelectedBrush();
        }
    }

    private static void GradientTypeChanged(AvaloniaPropertyChangedEventArgs<GradientType> args)
    {
        if (args.Sender is DualColorGradientPickerBase picker)
        {
            if (picker.isUpdating) return;

            picker.UpdateGradientType();
        }
    }

    private static void GradientStateChanged(AvaloniaPropertyChangedEventArgs<GradientState> args)
    {
        if (args.Sender is DualColorGradientPickerBase picker)
        {
            if (picker.isUpdating) return;
            picker.ignoreSelectedBrushChange = true;
            picker.UpdateGradientBrushFromState();
            picker.UpdateSelectedBrush();
            picker.NotifyableGradient?.UpdateEverything(args.OldValue.Value);
            picker.ignoreSelectedBrushChange = false;
        }
    }

    private static Avalonia.Matrix ToAvMatrix(Matrix matrix)
    {
        return new Avalonia.Matrix(matrix.ScaleX, matrix.SkewY, matrix.SkewX, matrix.ScaleY, matrix.TransX,
            matrix.TransY);
    }

    private static Matrix ToMatrix(Avalonia.Matrix matrix)
    {
        double scaleX = matrix.M11;
        double skewY = matrix.M12;
        double skewX = matrix.M21;
        double scaleY = matrix.M22;
        double offsetX = matrix.M31;
        double offsetY = matrix.M32;
        return new Matrix(scaleX, skewX, offsetX, skewY, scaleY, offsetY);
    }
}
