using ColorPicker.Models;
using System.Windows;
using Avalonia;

namespace ColorPicker
{
    public partial class SquarePicker : PickerControlBase
    {
        public static readonly StyledProperty<PickerType> PickerTypeProperty = AvaloniaProperty.Register<SquarePicker, PickerType>(
            nameof(PickerType), PickerType.HSV);

        public PickerType PickerType
        {
            get => GetValue(PickerTypeProperty);
            set => SetValue(PickerTypeProperty, value);
        }

        public static readonly StyledProperty<double> SmallChangeProperty = AvaloniaProperty.Register<SquarePicker, double>(
            nameof(SmallChange), 1.0);

        public double SmallChange
        {
            get => GetValue(SmallChangeProperty);
            set => SetValue(SmallChangeProperty, value);
        }

        public SquarePicker() : base()
        {
            InitializeComponent();
        }
    }
}
