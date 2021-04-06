using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ColorPicker.Behaviors
{
    internal class TextBoxFocusBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty FillSizeProperty =
            DependencyProperty.Register("FillSize", typeof(bool), typeof(TextBoxFocusBehavior),
                new PropertyMetadata(false));

        public bool FillSize
        {
            get => (bool)GetValue(FillSizeProperty);
            set => SetValue(FillSizeProperty, value);
        }

        //Converts number to proper format if enter is clicked and moves focus to next object
        private void AssociatedObject_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            Keyboard.ClearFocus();
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope(AssociatedObject), null);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotKeyboardFocus += AssociatedObjectGotKeyboardFocus;
            AssociatedObject.GotMouseCapture += AssociatedObjectGotMouseCapture;
            AssociatedObject.PreviewMouseLeftButtonDown += AssociatedObjectPreviewMouseLeftButtonDown;
            AssociatedObject.KeyUp += AssociatedObject_KeyUp;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotKeyboardFocus -= AssociatedObjectGotKeyboardFocus;
            AssociatedObject.GotMouseCapture -= AssociatedObjectGotMouseCapture;
            AssociatedObject.PreviewMouseLeftButtonDown -= AssociatedObjectPreviewMouseLeftButtonDown;
            AssociatedObject.KeyUp -= AssociatedObject_KeyUp;
        }

        private void AssociatedObjectGotKeyboardFocus(object sender,
            KeyboardFocusChangedEventArgs e)
        {
            AssociatedObject.SelectAll();
        }

        private void AssociatedObjectGotMouseCapture(object sender,
            MouseEventArgs e)
        {
            AssociatedObject.SelectAll();
        }

        private void AssociatedObjectPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!AssociatedObject.IsKeyboardFocusWithin)
            {
                AssociatedObject.Focus();
                e.Handled = true;
            }
        }
    }
}
