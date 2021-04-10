using ColorPicker.Models;
using System.Windows;
using System.Windows.Media;

namespace ColorPicker
{
    public class DualPickerControlBase : PickerControlBase, ISecondColorStorage
    {
        public static readonly DependencyProperty SecondColorStateProperty =
                DependencyProperty.Register(nameof(SecondColorState), typeof(ColorState), typeof(DualPickerControlBase),
                    new PropertyMetadata(new ColorState(1, 1, 1, 1, 0, 0, 1, 0, 0, 1), OnSecondColorStatePropertyChange));

        public static readonly DependencyProperty SecondaryColorProperty =
            DependencyProperty.Register(nameof(SecondaryColor), typeof(Color), typeof(DualPickerControlBase),
                new PropertyMetadata(Colors.White, OnSecondaryColorPropertyChange));

        private readonly SecondColorDecorator secondColorDecorator;
        public ColorState SecondColorState
        {
            get => (ColorState)GetValue(SecondColorStateProperty);
            set => SetValue(SecondColorStateProperty, value);
        }

        public NotifyableColor SecondColor
        {
            get;
            set;
        }
        public Color SecondaryColor
        {
            get => (Color)GetValue(SecondaryColorProperty);
            set => SetValue(SecondaryColorProperty, value);
        }

        public void SwapColors()
        {
            var temp = ColorState;
            ColorState = SecondColorState;
            SecondColorState = temp;
        }

        private bool ignoreSecondaryColorChange = false;
        private bool ignoreSecondaryColorPropertyChange = false;
        public DualPickerControlBase() : base()
        {
            secondColorDecorator = new SecondColorDecorator(this);
            SecondColor = new NotifyableColor(secondColorDecorator);
            SecondColor.PropertyChanged += (sender, args) =>
            {
                if (!ignoreSecondaryColorChange)
                {
                    ignoreSecondaryColorPropertyChange = true;
                    SecondaryColor = System.Windows.Media.Color.FromArgb((byte)SecondColor.A, (byte)SecondColor.RGB_R, (byte)SecondColor.RGB_G, (byte)SecondColor.RGB_B);
                    ignoreSecondaryColorPropertyChange = false;
                }
            };
        }
        private static void OnSecondColorStatePropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((DualPickerControlBase)d).SecondColor.UpdateEverything((ColorState)args.OldValue);
        }

        private static void OnSecondaryColorPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var sender = (DualPickerControlBase)d;
            if (sender.ignoreSecondaryColorPropertyChange)
                return;
            Color newValue = (Color)args.NewValue;
            sender.ignoreSecondaryColorChange = true;
            sender.SecondColor.A = newValue.A;
            sender.SecondColor.RGB_R = newValue.R;
            sender.SecondColor.RGB_G = newValue.G;
            sender.SecondColor.RGB_B = newValue.B;
            sender.ignoreSecondaryColorChange = false;
        }
    }
}
