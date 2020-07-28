using ColorPicker.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ColorPicker
{
    /// <summary>
    ///     Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class StandardColorPicker : UserControl, INotifyPropertyChanged
    {
        // Using a DependencyProperty as the backing store for SelectedColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(StandardColorPicker),
                new PropertyMetadata(Colors.Black, OnSelectedColorChanged));

        // Using a DependencyProperty as the backing store for SecondaryColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondaryColorProperty =
            DependencyProperty.Register("SecondaryColor", typeof(Color), typeof(StandardColorPicker),
                new PropertyMetadata(Colors.White));

        private readonly Image _colorPalette;


        private NotifyableColor _notifyableColor;

        public StandardColorPicker()
        {
            InitializeComponent();
            _colorPalette = FindName("colorPalette") as Image;
            NotifyableColor = new NotifyableColor(SelectedColor);
            NotifyableColor.ColorChanged += SelectedColor_ColorChanged;
        }

        public NotifyableColor NotifyableColor
        {
            get => _notifyableColor;
            set
            {
                _notifyableColor = value;
                RaisePropertyChanged("NotifyableColor");
            }
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

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void SelectedColor_ColorChanged(object sender, EventArgs e)
        {
            SelectedColor = Color.FromArgb(NotifyableColor.A, NotifyableColor.R, NotifyableColor.G, NotifyableColor.B);
        }

        private void SwapColors()
        {
            Color tmp = SecondaryColor;
            SecondaryColor = SelectedColor;
            SelectedColor = tmp;
        }

        private static void OnSelectedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Color color = (Color) e.NewValue;
            ((StandardColorPicker) d).NotifyableColor.SetArgb(color.A, color.R, color.G, color.B);
        }

        private void CalculateColor(Point pos)
        {
            pos.X = Math.Clamp(pos.X, 0, _colorPalette.ActualWidth);
            pos.Y = Math.Abs(Math.Clamp(pos.Y, 0, _colorPalette.ActualHeight) - _colorPalette.ActualHeight);
            int h = (int) (pos.X * 360f / _colorPalette.ActualWidth);
            float l = (float) (pos.Y * 100f / _colorPalette.ActualHeight);

            SelectedColor = HslConverter.HslToRGB(h, 100, l);
        }

        private void RaisePropertyChanged(string property)
        {
            if (property != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SwapColors();
        }

        private void colorPalette_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture((Image)sender);
        }

        private void colorPalette_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ((Image)sender).ReleaseMouseCapture();
        }

        private void colorPalette_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Released)
            {
                ((Image)sender).ReleaseMouseCapture();
                return;
            }

            Point point = e.GetPosition((Image)sender);
            CalculateColor(point);
        }
    }
}