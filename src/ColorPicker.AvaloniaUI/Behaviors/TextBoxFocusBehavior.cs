using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Avalonia.Xaml.Interactivity;

namespace ColorPicker.Behaviors;

public class TextBoxFocusBehavior : Behavior<TextBox>
{
    public static readonly StyledProperty<bool> SelectOnMouseClickProperty =
        AvaloniaProperty.Register<TextBoxFocusBehavior, bool>(
            nameof(SelectOnMouseClick),
            true);

    public static readonly StyledProperty<bool> ConfirmOnEnterProperty =
        AvaloniaProperty.Register<TextBoxFocusBehavior, bool>(
            nameof(ConfirmOnEnter),
            true);

    public static readonly StyledProperty<bool> DeselectOnFocusLossProperty =
        AvaloniaProperty.Register<TextBoxFocusBehavior, bool>(
            nameof(DeselectOnFocusLoss),
            true);

    public bool SelectOnMouseClick
    {
        get => GetValue(SelectOnMouseClickProperty);
        set => SetValue(SelectOnMouseClickProperty, value);
    }

    public bool ConfirmOnEnter
    {
        get => GetValue(ConfirmOnEnterProperty);
        set => SetValue(ConfirmOnEnterProperty, value);
    }

    public bool DeselectOnFocusLoss
    {
        get => GetValue(DeselectOnFocusLossProperty);
        set => SetValue(DeselectOnFocusLossProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.GotFocus += AssociatedObjectGotKeyboardFocus;
        AssociatedObject.LostFocus += AssociatedObject_LostFocus;
        AssociatedObject.AddHandler(InputElement.PointerPressedEvent, AssociatedObjectPointerPressed, RoutingStrategies.Tunnel);
        AssociatedObject.KeyUp += AssociatedObject_KeyUp;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.GotFocus -= AssociatedObjectGotKeyboardFocus;
        //AssociatedObject.GotMouseCapture -= AssociatedObjectGotMouseCapture;
        AssociatedObject.LostFocus -= AssociatedObject_LostFocus;
        AssociatedObject.PointerPressed -= AssociatedObjectPointerPressed;
        AssociatedObject.KeyUp -= AssociatedObject_KeyUp;
    }

    // Converts number to proper format if enter is clicked and moves focus to next object
    private void AssociatedObject_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter || !ConfirmOnEnter)
            return;

        RemoveFocus();
    }

    private void RemoveFocus()
    {
        var focusManager = TopLevel.GetTopLevel(AssociatedObject).FocusManager;
        var current = focusManager.GetFocusedElement();
        if (current != null)
        {
            //TODO: Find non obsolete way to do this
            var next = KeyboardNavigationHandler.GetNext(AssociatedObject, NavigationDirection.Next);
            next?.Focus(NavigationMethod.Tab);
        }
    }

    private void AssociatedObjectGotKeyboardFocus(
        object sender, GotFocusEventArgs e)
    {
        if (SelectOnMouseClick || e.NavigationMethod == NavigationMethod.Tab)
        {
            AssociatedObject?.Focus();
            AssociatedObject?.SelectAll();
        }
    }

    /*private void AssociatedObjectGotMouseCapture(
        object sender,
        MouseEventArgs e)
    {
        if (SelectOnMouseClick)
            AssociatedObject.SelectAll();
    }*/

    private void AssociatedObject_LostFocus(object sender, RoutedEventArgs e)
    {
        if (DeselectOnFocusLoss)
            AssociatedObject?.ClearSelection();
    }

    private void AssociatedObjectPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (!SelectOnMouseClick)
            return;

        if (!AssociatedObject.IsKeyboardFocusWithin)
        {
            AssociatedObject.Focus();
            if (SelectOnMouseClick)
                AssociatedObject.SelectAll();
            e.Handled = true;
        }
    }
}
