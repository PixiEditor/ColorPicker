using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;

namespace ColorPicker.Behaviors
{
    internal class TextBoxFocusBehavior : Behavior<TextBox>
    {
        public static readonly StyledProperty<bool> SelectOnMouseClickProperty =
            AvaloniaProperty.Register<TextBoxFocusBehavior, bool>(
                nameof(SelectOnMouseClick),
                defaultValue: true);

        public bool SelectOnMouseClick
        {
            get => GetValue(SelectOnMouseClickProperty);
            set => SetValue(SelectOnMouseClickProperty, value);
        }

        public static readonly StyledProperty<bool> ConfirmOnEnterProperty =
            AvaloniaProperty.Register<TextBoxFocusBehavior, bool>(
                nameof(ConfirmOnEnter),
                defaultValue: true);

        public bool ConfirmOnEnter
        {
            get => GetValue(ConfirmOnEnterProperty);
            set => SetValue(ConfirmOnEnterProperty, value);
        }

        public static readonly StyledProperty<bool> DeselectOnFocusLossProperty =
            AvaloniaProperty.Register<TextBoxFocusBehavior, bool>(
                nameof(DeselectOnFocusLoss),
                defaultValue: true);

        public bool DeselectOnFocusLoss
        {
            get => GetValue(DeselectOnFocusLossProperty);
            set => SetValue(DeselectOnFocusLossProperty, value);
        }

        protected override void OnAttached()
        {
            /*TODO: Make sure GotMouseCapture equivalent is handled*/
            base.OnAttached();
            AssociatedObject.GotFocus += AssociatedObjectGotKeyboardFocus;
            //AssociatedObject.GotMouseCapture += AssociatedObjectGotMouseCapture;
            AssociatedObject.LostFocus += AssociatedObject_LostFocus;
            AssociatedObject.PointerPressed += AssociatedObjectPointerPressed;
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
            /*DependencyObject scope = FocusManager.GetFocusScope(AssociatedObject);
            FrameworkElement parent = (FrameworkElement)AssociatedObject.Parent;

            while (parent != null && parent is IInputElement element && !element.Focusable)
            {
                parent = (FrameworkElement)parent.Parent;
            }

            FocusManager.SetFocusedElement(scope, parent);
            Keyboard.ClearFocus();*/

            IFocusManager focusManager = TopLevel.GetTopLevel(AssociatedObject).FocusManager;
            var current = focusManager.GetFocusedElement();
            if (current != null)
            {
                //TODO: Find non obsolete way to do this
                var next = KeyboardNavigationHandler.GetNext(current, NavigationDirection.Next);
                next?.Focus(NavigationMethod.Directional);
            }
        }

        private void AssociatedObjectGotKeyboardFocus(
            object sender, GotFocusEventArgs e)
        {
            if (SelectOnMouseClick || e.NavigationMethod == NavigationMethod.Tab)
                AssociatedObject?.SelectAll();
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
}
