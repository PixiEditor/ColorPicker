using ColorPicker.UserControls;
using System.Windows;

namespace ColorPicker
{
    public partial class HSVPicker : PickerControlBase
    {
        public static DependencyProperty PickerTypeProperty
            = DependencyProperty.Register(nameof(PickerType), typeof(PickerType), typeof(HSVPicker),
                new PropertyMetadata(PickerType.HSV));

        public static readonly DependencyProperty SmallChangeProperty =
            DependencyProperty.Register(nameof(SmallChange), typeof(double), typeof(HSVPicker),
                new PropertyMetadata(1.0));

        public PickerType PickerType
        {
            get => (PickerType)GetValue(PickerTypeProperty);
            set => SetValue(PickerTypeProperty, value);
        }

        public double SmallChange
        {
            get => (double)GetValue(SmallChangeProperty);
            set => SetValue(SmallChangeProperty, value);
        }

        public HSVPicker() : base()
        {
            InitializeComponent();
        }
    }
}
