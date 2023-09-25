using ColorPicker.Models;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Reactive;

namespace ColorPicker.UIExtensions
{
    internal abstract class PreviewColorSlider : Slider, INotifyPropertyChanged
    {
        public static readonly StyledProperty<ColorState> CurrentColorStateProperty =
            AvaloniaProperty.Register<PreviewColorSlider, ColorState>(
                nameof(CurrentColorState));

        public static readonly StyledProperty<double> SmallChangeBindableProperty = AvaloniaProperty.Register<PreviewColorSlider, double>(
            nameof(SmallChangeBindableProperty), 1);

        public double SmallChangeBindable
        {
            get => GetValue(SmallChangeProperty);
            set => SetValue(SmallChangeProperty, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool RefreshGradient => true;

        static PreviewColorSlider()
        {
            CurrentColorStateProperty.Changed.Subscribe(new AnonymousObserver<AvaloniaPropertyChangedEventArgs<ColorState>>(ColorStateChangedCallback));
            SmallChangeProperty.Changed.Subscribe(new AnonymousObserver<AvaloniaPropertyChangedEventArgs<double>>(SmallChangeBindableChangedCallback));
        }

        public PreviewColorSlider()
        {
            Minimum = 0;
            Maximum = 255;
            SmallChange = 1;
            LargeChange = 10;
            MinHeight = 12;
            PointerWheelChanged += OnPreviewMouseWheel;
        }

        public ColorState CurrentColorState
        {
            get => (ColorState)GetValue(CurrentColorStateProperty);
            set => SetValue(CurrentColorStateProperty, value);
        }

        private readonly LinearGradientBrush backgroundBrush = new LinearGradientBrush();
        public GradientStops BackgroundGradient
        {
            get => backgroundBrush.GradientStops;
            set => backgroundBrush.GradientStops = value;
        }

        private SolidColorBrush _leftCapColor = new SolidColorBrush();
        public SolidColorBrush LeftCapColor
        {
            get => _leftCapColor;
            set
            {
                _leftCapColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LeftCapColor)));
            }
        }

        private SolidColorBrush _rightCapColor = new SolidColorBrush();
        public SolidColorBrush RightCapColor
        {
            get => _rightCapColor;
            set
            {
                _rightCapColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RightCapColor)));
            }
        }

        public override void EndInit()
        {
            base.EndInit();
            Background = backgroundBrush;
            GenerateBackground();
        }

        protected abstract void GenerateBackground();
        protected static void ColorStateChangedCallback(AvaloniaPropertyChangedEventArgs<ColorState> e)
        {
            PreviewColorSlider slider = (PreviewColorSlider)e.Sender;
            if (slider.RefreshGradient)
                slider.GenerateBackground();
        }

        private static void SmallChangeBindableChangedCallback(AvaloniaPropertyChangedEventArgs<double> e)
        {
            ((PreviewColorSlider)e.Sender).SmallChange = e.NewValue.Value;
        }

        private void OnPreviewMouseWheel(object sender, PointerWheelEventArgs args)
        {
            Value = MathHelper.Clamp(Value + SmallChange * args.Delta.Y / 120, Minimum, Maximum);
            args.Handled = true;
        }
    }
}
