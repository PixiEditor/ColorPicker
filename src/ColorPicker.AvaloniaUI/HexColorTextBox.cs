using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Reactive;
using Avalonia.Xaml.Interactivity;
using ColorPicker.Behaviors;
using ColorPicker.Converters;
using ColorPicker.Models;

namespace ColorPicker;

[TemplatePart(Name = "PART_ResourcesContainer", Type = typeof(Control))]
[TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
public class HexColorTextBox : PickerControlBase
{
    public static readonly StyledProperty<bool> ShowAlphaProperty = AvaloniaProperty.Register<HexColorTextBox, bool>(
        nameof(ShowAlpha), true);

    public bool ShowAlpha
    {
        get => GetValue(ShowAlphaProperty);
        set => SetValue(ShowAlphaProperty, value);
    }

    public static readonly StyledProperty<HexRepresentationType> HexRepresentationProperty =
        AvaloniaProperty.Register<HexColorTextBox, HexRepresentationType>(
            nameof(HexRepresentation), HexRepresentationType.RGBA);

    public HexRepresentationType HexRepresentation
    {
        get => GetValue(HexRepresentationProperty);
        set => SetValue(HexRepresentationProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        // I feel like I'm terribly abusing the PART_ functionality and that I'm creating insanely non-obvious code
        // But I can't think of any other way of updating the text of the text box when ShowAlpha and HexRepresentation change

        if (!e.NameScope.Find<Control>("PART_ResourcesContainer").Resources
                .TryGetValue("ColorToHexConverter", out object converter) ||
            converter is not ColorToHexConverter colorToHexConverter)
            return;
        if (e.NameScope.Find<TextBox>("PART_TextBox") is not TextBox textbox)
            return;

        EventHandler onChange = (sender, args) =>
        {
            var converted = colorToHexConverter.Convert(SelectedColor, null, null, null) as string;
            if (converted is not null)
                textbox.Text = converted;
        };

        colorToHexConverter.OnShowAlphaChange += onChange;
        colorToHexConverter.OnShowHexRepresentationChange += onChange;
    }
}
