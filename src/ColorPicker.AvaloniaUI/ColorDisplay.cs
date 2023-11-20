using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace ColorPicker;

[TemplatePart(Name = "PART_SwapButton", Type = typeof(Button))]
[TemplatePart(Name = "PART_HintColor", Type = typeof(Control))]
public class ColorDisplay : DualPickerControlBase
{
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var button = e.NameScope.Find<Button>("PART_SwapButton");
        button.Click += SwapButton_Click;

        InputElement hintColor = e.NameScope.Find<Control>("PART_HintColor");
        hintColor.AddHandler(PointerPressedEvent, HintColor_PointerPressed, RoutingStrategies.Tunnel);
    }

    private void SwapButton_Click(object sender, RoutedEventArgs e)
    {
        SwapColors();
    }

    private void HintColor_PointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed) SetMainColorFromHintColor();
    }
}