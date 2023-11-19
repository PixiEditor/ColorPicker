using System.Windows;
using ColorPicker.Models;

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

        public static readonly DependencyProperty PickerTypeProperty
            = DependencyProperty.Register(nameof(PickerType), typeof(PickerType), typeof(StandardColorPicker),
                new PropertyMetadata(PickerType.HSV));

        public static readonly DependencyProperty ShowFractionalPartProperty =
            DependencyProperty.Register(nameof(ShowFractionalPart), typeof(bool), typeof(StandardColorPicker),
                new PropertyMetadata(true));
        
        public static readonly DependencyProperty HexRepresentationProperty = 
            DependencyProperty.Register(nameof(HexRepresentation), typeof(HexRepresentationType), typeof(StandardColorPicker),
                new PropertyMetadata(HexRepresentationType.RGBA));

        public HexRepresentationType HexRepresentation
        {
            get => (HexRepresentationType)GetValue(HexRepresentationProperty);
            set => SetValue(HexRepresentationProperty, value);
        }

        public StandardColorPicker()
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

        public PickerType PickerType
        {
            get => (PickerType)GetValue(PickerTypeProperty);
            set => SetValue(PickerTypeProperty, value);
        }

        public bool ShowFractionalPart
        {
            get => (bool)GetValue(ShowFractionalPartProperty);
            set => SetValue(ShowFractionalPartProperty, value);
        }
    }
}