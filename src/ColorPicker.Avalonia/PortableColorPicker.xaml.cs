using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace ColorPicker
{
    /// <summary>
    ///     Interaction logic for PortableColorPicker.xaml
    /// </summary>
    public partial class PortableColorPicker : UserControl
    {
        // Using a DependencyProperty as the backing store for SelectedColor.  This enables animation, styling, binding, etc...
        public static readonly StyledProperty<Color> SelectedColorProperty =
            AvaloniaProperty.Register<PortableColorPicker, Color>("SelectedColor");

        // Using a DependencyProperty as the backing store for SecondaryColor.  This enables animation, styling, binding, etc...
        public static readonly StyledProperty<Color> SecondaryColorProperty =
            AvaloniaProperty.Register<PortableColorPicker, Color>("SecondaryColor");

        public PortableColorPicker()
        {
            AvaloniaXamlLoader.Load(this);
        }


        public Color SelectedColor
        {
            get => GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }


        public Color SecondaryColor
        {
            get => GetValue(SecondaryColorProperty);
            set => SetValue(SecondaryColorProperty, value);
        }
    }
}