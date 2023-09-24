using ColorPicker.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace ColorPicker
{
    internal partial class HueSlider : UserControl
    {
        public static readonly StyledProperty<double> SmallChangeProperty = AvaloniaProperty.Register<HueSlider, double>(
            nameof(SmallChange), 1);

        public static readonly StyledProperty<double> ValueProperty = AvaloniaProperty.Register<HueSlider, double>(
            nameof(Value), 0);

        public double SmallChange
        {
            get => GetValue(SmallChangeProperty);
            set => SetValue(SmallChangeProperty, value);
        }

        public double Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public HueSlider()
        {
            InitializeComponent();
        }

        private void OnMouseDown(object sender, PointerPressedEventArgs e)
        {
            Avalonia.Controls.Shapes.Path circle = (Avalonia.Controls.Shapes.Path)sender;
            e.Pointer.Capture(circle);
            Point mousePos = e.GetPosition(circle);
            UpdateValue(mousePos, circle.Bounds.Width, circle.Bounds.Height);
        }

        private void OnMouseUp(object sender, PointerReleasedEventArgs e)
        {
            e.Pointer.Capture(null);
        }

        private void OnMouseMove(object sender, PointerEventArgs e)
        {
            if (e.Pointer.Captured == null)
                return;
            Avalonia.Controls.Shapes.Path circle = (Avalonia.Controls.Shapes.Path)sender;
            Point mousePos = e.GetPosition(circle);
            UpdateValue(mousePos, circle.Bounds.Width, circle.Bounds.Height);
        }

        private void UpdateValue(Point mousePos, double width, double height)
        {
            double x = mousePos.X / (width * 2);
            double y = mousePos.Y / (height * 2);

            double length = Math.Sqrt(x * x + y * y);
            if (length == 0)
                return;
            double angle = Math.Acos(x / length);
            if (y < 0)
                angle = -angle;
            angle = angle * 360 / (Math.PI * 2) + 180;
            Value = MathHelper.Clamp(angle, 0, 360);
        }

        private void OnPreviewMouseWheel(object sender, PointerWheelEventArgs args)
        {
            Value = MathHelper.Mod(Value + SmallChange * args.Delta.Y / 120, 360);
            args.Handled = true;
        }
    }
}
