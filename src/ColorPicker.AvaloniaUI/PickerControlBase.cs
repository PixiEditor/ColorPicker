using ColorPicker.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Reactive;

namespace ColorPicker
{
    public class PickerControlBase : UserControl, IColorStateStorage
    {
        public static readonly StyledProperty<ColorState> ColorStateProperty = AvaloniaProperty.Register<PickerControlBase, ColorState>(
            nameof(ColorState), new ColorState(0, 0, 0, 1, 0, 0, 0, 0, 0, 0));

        public ColorState ColorState
        {
            get => GetValue(ColorStateProperty);
            set => SetValue(ColorStateProperty, value);
        }

        public static readonly StyledProperty<Color> SelectedColorProperty = AvaloniaProperty.Register<PickerControlBase, Color>(
            nameof(SelectedColor), Colors.Black);

        public Color SelectedColor
        {
            get => GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public static readonly RoutedEvent ColorChangedEvent = RoutedEvent.Register<PickerControlBase, ColorRoutedEventArgs>(
            nameof(ColorChanged), RoutingStrategies.Bubble);

        public NotifyableColor Color
        {
            get;
            set;
        }

        private bool ignoreColorPropertyChange = false;
        private bool ignoreColorChange = false;
        private Color previousColor = Avalonia.Media.Color.FromArgb(5, 5, 5, 5);

        public event EventHandler<RoutedEventArgs> ColorChanged
        {
            add => AddHandler(ColorChangedEvent, value);
            remove => RemoveHandler(ColorChangedEvent, value);
        }

        static PickerControlBase()
        {
            ColorStateProperty.Changed.Subscribe(new AnonymousObserver<AvaloniaPropertyChangedEventArgs<ColorState>>(OnColorStatePropertyChange));
            SelectedColorProperty.Changed.Subscribe(new AnonymousObserver<AvaloniaPropertyChangedEventArgs<Color>>(OnSelectedColorPropertyChange));
        }

        public PickerControlBase()
        {
            Color = new NotifyableColor(this);
            Color.PropertyChanged += (sender, args) =>
            {
                var newColor = Avalonia.Media.Color.FromArgb(
                    (byte)Math.Round(Color.A),
                    (byte)Math.Round(Color.RGB_R),
                    (byte)Math.Round(Color.RGB_G),
                    (byte)Math.Round(Color.RGB_B));
                if (newColor != previousColor)
                {
                    RaiseEvent(new ColorRoutedEventArgs(ColorChangedEvent, newColor));
                    previousColor = newColor;
                }
            };
            ColorChanged += (sender, newColor) =>
            {
                if (!ignoreColorChange)
                {
                    ignoreColorPropertyChange = true;
                    SelectedColor = ((ColorRoutedEventArgs)newColor).Color;
                    ignoreColorPropertyChange = false;
                }
            };
        }

        private static void OnColorStatePropertyChange(AvaloniaPropertyChangedEventArgs<ColorState> args)
        {
            ((PickerControlBase)args.Sender).Color.UpdateEverything(args.OldValue.Value);
        }

        private static void OnSelectedColorPropertyChange(AvaloniaPropertyChangedEventArgs<Color> args)
        {
            var sender = (PickerControlBase)args.Sender;
            if (sender.ignoreColorPropertyChange)
                return;
            Color newValue = args.NewValue.Value;
            sender.ignoreColorChange = true;
            sender.Color.A = newValue.A;
            sender.Color.RGB_R = newValue.R;
            sender.Color.RGB_G = newValue.G;
            sender.Color.RGB_B = newValue.B;
            sender.ignoreColorChange = false;
        }
    }
}
