using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker
{
    public partial class PortableColorPicker : UserControl
    {
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(PortableColorPicker),
                new PropertyMetadata(Colors.Black));

        public static readonly DependencyProperty SecondaryColorProperty =
            DependencyProperty.Register(nameof(SecondaryColor), typeof(Color), typeof(PortableColorPicker),
                new PropertyMetadata(Colors.White));

        public static readonly DependencyProperty HueSmallChangeProperty =
            DependencyProperty.Register(nameof(HueSmallChange), typeof(double), typeof(PortableColorPicker),
                new PropertyMetadata(1.0));

        public static readonly DependencyProperty SmallChangeProperty =
            DependencyProperty.Register(nameof(SmallChange), typeof(double), typeof(PortableColorPicker),
                new PropertyMetadata(0.00390625));

        public double SmallChange
        {
            get => (double)GetValue(SmallChangeProperty);
            set => SetValue(SmallChangeProperty, value);
        }

        public double HueSmallChange
        {
            get => (double)GetValue(HueSmallChangeProperty);
            set => SetValue(HueSmallChangeProperty, value);
        }

        public PortableColorPicker()
        {
            InitializeComponent();
        }


        public Color SelectedColor
        {
            get => (Color) GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }


        public Color SecondaryColor
        {
            get => (Color) GetValue(SecondaryColorProperty);
            set => SetValue(SecondaryColorProperty, value);
        }
    }
}