using ColorPicker.Models;
using System.Windows;
using Avalonia;

namespace ColorPicker
{
    public partial class StandardColorPicker : DualPickerControlBase
    {
        public static readonly StyledProperty<double> SmallChangeProperty =
            AvaloniaProperty.Register<StandardColorPicker, double>(
                nameof(SmallChange),
                defaultValue: 1.0);

        public double SmallChange
        {
            get => GetValue(SmallChangeProperty);
            set => SetValue(SmallChangeProperty, value);
        }

        public static readonly StyledProperty<bool> ShowAlphaProperty =
            AvaloniaProperty.Register<StandardColorPicker, bool>(
                nameof(ShowAlpha),
                defaultValue: true);

        public bool ShowAlpha
        {
            get => GetValue(ShowAlphaProperty);
            set => SetValue(ShowAlphaProperty, value);
        }

        public static readonly StyledProperty<PickerType> PickerTypeProperty =
            AvaloniaProperty.Register<StandardColorPicker, PickerType>(
                nameof(PickerType),
                defaultValue: PickerType.HSV);

        public PickerType PickerType
        {
            get => GetValue(PickerTypeProperty);
            set => SetValue(PickerTypeProperty, value);
        }

        public static readonly StyledProperty<bool> ShowFractionalPartProperty =
            AvaloniaProperty.Register<StandardColorPicker, bool>(
                nameof(ShowFractionalPart),
                defaultValue: true);

        public bool ShowFractionalPart
        {
            get => GetValue(ShowFractionalPartProperty);
            set => SetValue(ShowFractionalPartProperty, value);
        }

        public StandardColorPicker() : base()
        {
            InitializeComponent();
        }
    }
}
