using ColorPicker.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker
{
    public class PickerControlBase : UserControl, IColorStateStorage
    {
        public static DependencyProperty ColorStateProperty =
            DependencyProperty.Register(nameof(ColorState), typeof(ColorState), typeof(PickerControlBase),
                new PropertyMetadata(new ColorState(0, 0, 0, 1, 0, 0, 0, 0, 0, 0), OnColorStatePropertyChange));

        public static DependencyProperty SelectedColorProperty =
            DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(PickerControlBase),
                new PropertyMetadata(Colors.Black, OnSelectedColorPropertyChange));

        public ColorState ColorState
        {
            get => (ColorState)GetValue(ColorStateProperty);
            set => SetValue(ColorStateProperty, value);
        }
        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public NotifyableColor Color
        {
            get;
            set;
        }

        private bool ignoreColorPropertyChange = false;
        private bool ignoreColorChange = false;
        public event EventHandler<Color> ColorChanged;

        public PickerControlBase()
        {
            Color = new NotifyableColor(this);
            Color.PropertyChanged += (sender, args) =>
            {
                ColorChanged?.Invoke(this, System.Windows.Media.Color.FromArgb((byte)Color.A, (byte)Color.RGB_R, (byte)Color.RGB_G, (byte)Color.RGB_B));
            };
            ColorChanged += (sender, newColor) =>
            {
                if (!ignoreColorChange)
                {
                    ignoreColorPropertyChange = true;
                    SelectedColor = newColor;
                    ignoreColorPropertyChange = false;
                }
            };
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
            Color newValue = (Color)args.NewValue;
            sender.ignoreColorChange = true;
            sender.Color.A = newValue.A;
            sender.Color.RGB_R = newValue.R;
            sender.Color.RGB_G = newValue.G;
            sender.Color.RGB_B = newValue.B;
            sender.ignoreColorChange = false;
        }
    }
}
