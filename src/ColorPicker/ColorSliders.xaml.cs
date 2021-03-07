using System.Windows;

namespace ColorPicker
{
    public partial class ColorSliders : PickerControlBase
    {
        public static readonly DependencyProperty SmallChangeProperty =
            DependencyProperty.Register(nameof(SmallChange), typeof(double), typeof(ColorSliders),
                new PropertyMetadata(1.0));
        public double SmallChange
        {
            get => (double)GetValue(SmallChangeProperty);
            set => SetValue(SmallChangeProperty, value);
        }
        public ColorSliders() : base()
        {
            InitializeComponent();
        }
    }
}
