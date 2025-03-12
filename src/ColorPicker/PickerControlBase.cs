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