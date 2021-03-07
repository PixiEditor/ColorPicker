using ColorPicker.Models;
using System.Windows;

namespace ColorPicker
{
    public class DualPickerControlBase : PickerControlBase, ISecondColorStorage
    {
        public static readonly DependencyProperty SecondColorStateProperty =
                DependencyProperty.Register(nameof(SecondColorState), typeof(ColorState), typeof(DualPickerControlBase),
                    new PropertyMetadata(new ColorState(1, 1, 1, 1, 0, 0, 1), OnSecondColorStatePropertyChange));
        private SecondColorDecorator secondColorDecorator;
        public ColorState SecondColorState
        {
            get => (ColorState)GetValue(SecondColorStateProperty);
            set => SetValue(SecondColorStateProperty, value);
        }

        public NotifyableColor SecondColor
        {
            get;
            set;
        }
        public DualPickerControlBase() : base()
        {
            secondColorDecorator = new SecondColorDecorator(this);
            SecondColor = new NotifyableColor(secondColorDecorator);
        }
        private static void OnSecondColorStatePropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((DualPickerControlBase)d).SecondColor.UpdateEverything((ColorState)args.OldValue);
        }
    }
}
