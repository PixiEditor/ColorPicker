using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ColorPicker.Models;

namespace ColorPicker
{
    public class PickerControlBase : UserControl, IColorStateStorage
    {
        public static readonly DependencyProperty ColorStateProperty =
            DependencyProperty.Register(nameof(ColorState), typeof(ColorState), typeof(PickerControlBase),
                new PropertyMetadata(new ColorState(0, 0, 0, 1, 0, 0, 0, 0, 0, 0), OnColorStatePropertyChange));

        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(PickerControlBase),
                new PropertyMetadata(Colors.Black, OnSelectedColorPropertyChange));
        
        public static readonly RoutedEvent ColorChangedEvent =
            EventManager.RegisterRoutedEvent(nameof(ColorChanged),
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PickerControlBase));

        private bool ignoreColorChange;

        private bool ignoreColorPropertyChange;
        private Color previousColor = System.Windows.Media.Color.FromArgb(5, 5, 5, 5);

        public PickerControlBase()
        {
            Color = new NotifyableColor(this);
            Color.UpdateAllCompleted += (sender, args) =>
            {
                var newColor = System.Windows.Media.Color.FromArgb(
                    (byte)Math.Round(Color.A),
                    (byte)Math.Round(Color.RGB_R),
                    (byte)Math.Round(Color.RGB_G),
                    (byte)Math.Round(Color.RGB_B));
                if(newColor != previousColor)
                {
                    RaiseEvent(new ColorRoutedEventArgs(ColorChangedEvent, newColor));
                    previousColor = newColor;
                }
            };

            var updateColorAction = new Action<object, RoutedEventArgs>
                ((sender, newColor) =>
            {
                if (!ignoreColorChange)
                {
                    ignoreColorPropertyChange = true;
                    if (IsEnabled)
                        SelectedColor = ((ColorRoutedEventArgs)newColor).Color;
                    else
                    {
                        var grayColor = ((ColorRoutedEventArgs)newColor).Color.R * 0.21
                                        + ((ColorRoutedEventArgs)newColor).Color.G * 0.72
                                        + ((ColorRoutedEventArgs)newColor).Color.B * 0.07;
                        SelectedColor = System.Windows.Media.Color.FromArgb(
                            ((ColorRoutedEventArgs)newColor).Color.A,
                            (byte)grayColor,
                            (byte)grayColor,
                            (byte)grayColor);
                    }
                    ignoreColorPropertyChange = false;
                }
            });

            ColorChanged += (sender, args) => updateColorAction(sender, args);
            IsEnabledChanged += (sender, args) =>
            {
                var color = System.Windows.Media.Color.FromArgb(
                    (byte)Math.Round(Color.A),
                    (byte)Math.Round(Color.RGB_R),
                    (byte)Math.Round(Color.RGB_G),
                    (byte)Math.Round(Color.RGB_B));

                updateColorAction(sender, new ColorRoutedEventArgs(ColorChangedEvent, color));
            };
        }

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public NotifyableColor Color { get; set; }

        public ColorState ColorState
        {
            get => (ColorState)GetValue(ColorStateProperty);
            set => SetValue(ColorStateProperty, value);
        }

        public event RoutedEventHandler ColorChanged
        {
            add => AddHandler(ColorChangedEvent, value);
            remove => RemoveHandler(ColorChangedEvent, value);
        }

        private static void OnColorStatePropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {

            ((PickerControlBase)d).Color.UpdateEverything((ColorState)args.OldValue);
        }

        private static void OnSelectedColorPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var sender = (PickerControlBase)d;
            if (sender.ignoreColorPropertyChange)
                return;
            var newValue = (Color)args.NewValue;
            sender.ignoreColorChange = true;
            sender.Color.A = newValue.A;
            sender.Color.RGB_R = newValue.R;
            sender.Color.RGB_G = newValue.G;
            sender.Color.RGB_B = newValue.B;
            sender.ignoreColorChange = false;
        }
    }
}