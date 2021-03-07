using System.Windows;

namespace ColorPicker
{
    public partial class PortableColorPicker : DualPickerControlBase
    {
        public static readonly DependencyProperty SmallChangeProperty =
            DependencyProperty.Register(nameof(SmallChange), typeof(double), typeof(PortableColorPicker),
                new PropertyMetadata(1.0));

        public double SmallChange
        {
            get => (double)GetValue(SmallChangeProperty);
            set => SetValue(SmallChangeProperty, value);
        }

        public PortableColorPicker()
        {
            InitializeComponent();
        }
    }
}
