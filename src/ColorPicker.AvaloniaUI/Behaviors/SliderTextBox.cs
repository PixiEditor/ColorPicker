using Avalonia;
using Avalonia.Controls;
using Avalonia.Reactive;

namespace ColorPicker.Behaviors;

public class SliderTextBox : AvaloniaObject
{
    public static readonly AttachedProperty<bool> ShowFractionalPartProperty =
        AvaloniaProperty.RegisterAttached<SliderTextBox, TextBox, bool>("ShowFractionalPart");

    public static void SetShowFractionalPart(TextBox obj, bool value) => obj.SetValue(ShowFractionalPartProperty, value);
    public static bool GetShowFractionalPart(TextBox obj) => obj.GetValue(ShowFractionalPartProperty);

    public static readonly AttachedProperty<double> PreciseValueProperty =
        AvaloniaProperty.RegisterAttached<SliderTextBox, TextBox, double>("PreciseValue");

    public static void SetPreciseValue(TextBox obj, double value) => obj.SetValue(PreciseValueProperty, value);
    public static double GetPreciseValue(TextBox obj) => obj.GetValue(PreciseValueProperty);


    static SliderTextBox()
    {
        ShowFractionalPartProperty.Changed.Subscribe(new AnonymousObserver<AvaloniaPropertyChangedEventArgs<bool>>(ShowFractionalPartChangedCallback));
        PreciseValueProperty.Changed.Subscribe(new AnonymousObserver<AvaloniaPropertyChangedEventArgs<double>>(PreciseValueChangedCallback));
    }

    private static void PreciseValueChangedCallback(AvaloniaPropertyChangedEventArgs<double> value)
    {
        TextBox tb = (TextBox)value.Sender;
        tb.Text = value.NewValue.Value.ToString(GetShowFractionalPart(tb) ? "N1" : "N0");
    }

    private static void ShowFractionalPartChangedCallback(AvaloniaPropertyChangedEventArgs<bool> obj)
    {
        TextBox tb = (TextBox)obj.Sender;

        if (obj.NewValue.Value)
        {
            tb.Text = GetPreciseValue(tb).ToString("N1");
        }
        else
        {
            tb.Text = GetPreciseValue(tb).ToString("N0");
        }
    }
}