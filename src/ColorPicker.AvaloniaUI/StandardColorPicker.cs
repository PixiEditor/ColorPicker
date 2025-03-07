using System.Collections.Specialized;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;
using ColorPicker.Models;
using GradientStop = ColorPicker.Models.GradientStop;

namespace ColorPicker;

[TemplatePart("PART_TabControl", typeof(TabControl))]
public class StandardColorPicker : DualColorGradientPickerBase
{
    public static readonly StyledProperty<double> SmallChangeProperty =
        AvaloniaProperty.Register<StandardColorPicker, double>(
            nameof(SmallChange),
            1.0);

    public static readonly StyledProperty<bool> ShowAlphaProperty =
        AvaloniaProperty.Register<StandardColorPicker, bool>(
            nameof(ShowAlpha),
            true);

    public static readonly StyledProperty<PickerType> PickerTypeProperty =
        AvaloniaProperty.Register<StandardColorPicker, PickerType>(
            nameof(PickerType),
            PickerType.HSV);

    public static readonly StyledProperty<bool> ShowFractionalPartProperty =
        AvaloniaProperty.Register<StandardColorPicker, bool>(
            nameof(ShowFractionalPart),
            true);

    public static readonly StyledProperty<HexRepresentationType> HexRepresentationProperty =
        AvaloniaProperty.Register<StandardColorPicker, HexRepresentationType>(
            nameof(HexRepresentation), HexRepresentationType.RGBA);

    public HexRepresentationType HexRepresentation
    {
        get => GetValue(HexRepresentationProperty);
        set => SetValue(HexRepresentationProperty, value);
    }

    public double SmallChange
    {
        get => GetValue(SmallChangeProperty);
        set => SetValue(SmallChangeProperty, value);
    }

    public bool ShowAlpha
    {
        get => GetValue(ShowAlphaProperty);
        set => SetValue(ShowAlphaProperty, value);
    }

    public PickerType PickerType
    {
        get => GetValue(PickerTypeProperty);
        set => SetValue(PickerTypeProperty, value);
    }

    public bool ShowFractionalPart
    {
        get => GetValue(ShowFractionalPartProperty);
        set => SetValue(ShowFractionalPartProperty, value);
    }

    private TabControl tabControl;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        tabControl = e.NameScope.Find<TabControl>("PART_TabControl");
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        if (tabControl != null && !EnableGradientsTab)
        {
            tabControl.FindDescendantOfType<TabItem>().IsVisible = false;
        }
    }
}
