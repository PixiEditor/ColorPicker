using System.Windows;

namespace ColorPicker
{
    public partial class StandardColorPicker : DualPickerControlBase
    {
        public static readonly DependencyProperty SmallChangeProperty =
            DependencyProperty.Register(nameof(SmallChange), typeof(double), typeof(StandardColorPicker),
                new PropertyMetadata(1.0));

        public static readonly DependencyProperty ShowAlphaProperty =
            DependencyProperty.Register(nameof(ShowAlpha), typeof(bool), typeof(StandardColorPicker),
                new PropertyMetadata(true));

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

        public StandardColorPicker() : base()
        {
            InitializeComponent();
        }
    }
}
