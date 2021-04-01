using ColorPicker.UserControls;
using System.Windows;

namespace ColorPicker
{
    public partial class HSVPicker : PickerControlBase
    {
        public static DependencyProperty PickerTypeProperty
            = DependencyProperty.Register(nameof(PickerType), typeof(PickerType), typeof(HSVPicker),
                new PropertyMetadata(PickerType.HSV));

        public PickerType PickerType
        {
            get => (PickerType)GetValue(PickerTypeProperty);
            set => SetValue(PickerTypeProperty, value);
        }
        public HSVPicker() : base()
        {
            InitializeComponent();
        }
    }
}
