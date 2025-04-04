using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Reactive;
using Avalonia.Xaml.Interactivity;

namespace ColorPicker.Behaviors;

public class LostFocusUpdateBindingBehavior : Behavior<TextBox>
{
    static LostFocusUpdateBindingBehavior()
    {
        TextProperty.Changed.Subscribe(new AnonymousObserver<AvaloniaPropertyChangedEventArgs<string>>(e =>
        {
            ((LostFocusUpdateBindingBehavior) e.Sender).OnBindingValueChanged();
        }));
    }


    public static readonly StyledProperty<string> TextProperty = AvaloniaProperty.Register<LostFocusUpdateBindingBehavior, string>(
        "Text", defaultBindingMode: BindingMode.TwoWay);

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    protected override void OnAttached()
    {
        AssociatedObject.LostFocus += OnLostFocus;
        OnBindingValueChanged();
        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        AssociatedObject.LostFocus -= OnLostFocus;
        base.OnDetaching();
    }

    private void OnLostFocus(object sender, RoutedEventArgs e)
    {
        if (AssociatedObject != null)
        {
            string oldValue = Text;
            Text = AssociatedObject.Text;
            OnSubmitValue(oldValue, Text);
        }
    }

    protected void OnBindingValueChanged()
    {
        if (AssociatedObject != null)
        {
            AssociatedObject.Text = Text;
        }
    }

    protected virtual void OnSubmitValue(string oldValue, string newValue)
    {

    }
}
