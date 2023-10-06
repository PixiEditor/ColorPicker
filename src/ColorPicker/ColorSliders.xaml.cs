using System.Windows;

namespace ColorPicker
{
    public partial class ColorSliders : PickerControlBase
    {
        public static readonly DependencyProperty SmallChangeProperty =
            DependencyProperty.Register(nameof(SmallChange), typeof(double), typeof(ColorSliders),
                new PropertyMetadata(1.0));

        public static readonly DependencyProperty ShowAlphaProperty =
            DependencyProperty.Register(nameof(ShowAlpha), typeof(bool), typeof(ColorSliders),
                new PropertyMetadata(true));

        public static readonly DependencyProperty ShowFractionalPartProperty =
            DependencyProperty.Register(nameof(ShowFractionalPart), typeof(bool), typeof(ColorSliders),
                new PropertyMetadata(true));

        public ColorSliders()
        {
            InitializeComponent();
        }

        public double SmallChange
        {
            get => (double)GetValue(SmallChangeProperty);
            set => SetValue(SmallChangeProperty, value);
        }

        public bool ShowAlpha
        {
            get => (bool)GetValue(ShowAlphaProperty);
            set => SetValue(ShowAlphaProperty, value);
        }

        public bool ShowFractionalPart
        {
            get => (bool)GetValue(ShowFractionalPartProperty);
            set => SetValue(ShowFractionalPartProperty, value);
        }
    }
}