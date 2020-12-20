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