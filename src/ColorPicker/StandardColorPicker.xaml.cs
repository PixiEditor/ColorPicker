using System.Windows;

namespace ColorPicker
{
    public partial class StandardColorPicker : DualPickerControlBase
    {
        public static readonly DependencyProperty SmallChangeProperty =
            DependencyProperty.Register(nameof(SmallChange), typeof(double), typeof(StandardColorPicker),
                new PropertyMetadata(1.0));

        public double SmallChange
        {
            get => (double)GetValue(SmallChangeProperty);
            set => SetValue(SmallChangeProperty, value);
        }

        public StandardColorPicker() : base()
        {
            InitializeComponent();
        }
    }
}
