using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using ColorPicker.Models;
using ReactiveUI;
using System;

namespace ColorPicker
{
    /// <summary>
    ///     Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class StandardColorPicker : UserControl
    {
        // Using a DependencyProperty as the backing store for SelectedColor.  This enables animation, styling, binding, etc...
        public static readonly StyledProperty<Color> SelectedColorProperty =
            AvaloniaProperty.Register<PortableColorPicker, Color>("SelectedColor");

        // Using a DependencyProperty as the backing store for SecondaryColor.  This enables animation, styling, binding, etc...
        public static readonly StyledProperty<Color> SecondaryColorProperty =
            AvaloniaProperty.Register<PortableColorPicker, Color>("SecondaryColor");

        private readonly Image _colorPalette;

        public StandardColorPicker()
        {
            AvaloniaXamlLoader.Load(this);
            _colorPalette = this.Find<Image>("colorPalette");
            NotifyableColor = new NotifyableColor(SelectedColor);

            this.WhenAnyValue(x => x.SelectedColor).Subscribe(x => NotifyableColor.SetArgb(x.A, x.R, x.G, x.B));
            this.WhenAnyValue(x => x.NotifyableColor).Subscribe(x => 
            SelectedColor = Color.FromArgb(NotifyableColor.A, NotifyableColor.R, NotifyableColor.G, NotifyableColor.B));
        }

        private void StandardColorPicker_PointerMoved(object sender, Avalonia.Input.PointerEventArgs e)
        {
            Point point = e.GetPosition((Image)sender);
            CalculateColor(point);
        }

        private void StandardColorPicker_PointerReleased(object sender, Avalonia.Input.PointerReleasedEventArgs e)
        {
            e.Pointer.Capture(null);
        }

        private void StandardColorPicker_PointerPressed(object sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            e.Pointer.Capture((Image)sender);
        }

        public NotifyableColor NotifyableColor { get; set; }


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

        private void SwapColors()
        {
            Color tmp = SecondaryColor;
            SecondaryColor = SelectedColor;
            SelectedColor = tmp;
        }

        private void CalculateColor(Point pos)
        {
            pos = new Point(Math.Clamp(pos.X, 0, _colorPalette.Width), 
                Math.Abs(Math.Clamp(pos.Y, 0, _colorPalette.Height) - _colorPalette.Height));
            int h = (int) (pos.X * 360f / _colorPalette.Width);
            float l = (float) (pos.Y * 100f / _colorPalette.Height);

            SelectedColor = HslConverter.HslToRGB(h, 100, l);
        }
    }
}