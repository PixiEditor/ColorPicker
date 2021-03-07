using ColorPicker.Models;
using System.Windows;

namespace ColorPicker
{
    public partial class ColorDisplay : PickerControlBase
    {
        public static readonly DependencyProperty SecondColorStateProperty =
            DependencyProperty.Register(nameof(SecondColorState), typeof(ColorState), typeof(ColorDisplay),
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
        public ColorDisplay() : base()
        {
            secondColorDecorator = new SecondColorDecorator(this);
            SecondColor = new NotifyableColor(secondColorDecorator);
            InitializeComponent();
        }

        private void SwapButton_Click(object sender, RoutedEventArgs e)
        {
            var temp = ColorState;
            ColorState = SecondColorState;
            SecondColorState = temp;
        }
        private static void OnSecondColorStatePropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((ColorDisplay)d).SecondColor.UpdateEverything((ColorState)args.OldValue);
        }
    }
}
