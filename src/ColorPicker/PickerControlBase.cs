using ColorPicker.Models;
using System.Windows;
using System.Windows.Controls;

namespace ColorPicker
{
    public class PickerControlBase : UserControl, IColorStateStorage
    {
        public static DependencyProperty ColorStateProperty =
            DependencyProperty.Register(nameof(ColorState), typeof(ColorState), typeof(PickerControlBase),
                new PropertyMetadata(new ColorState(0, 0, 0, 1, 0, 0, 0, 0, 0, 0), OnColorStatePropertyChange));

        public ColorState ColorState
        {
            get => (ColorState)GetValue(ColorStateProperty);
            set => SetValue(ColorStateProperty, value);
        }

        public NotifyableColor Color
        {
            get;
            set;
        }

        public PickerControlBase()
        {
            Color = new NotifyableColor(this);
        }

        private static void OnColorStatePropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((PickerControlBase)d).Color.UpdateEverything((ColorState)args.OldValue);
        }
    }
}
