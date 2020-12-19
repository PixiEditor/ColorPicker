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
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(StandardColorPicker),
                new PropertyMetadata(Colors.Black, OnSelectedColorChanged));

        public static readonly DependencyProperty SecondaryColorProperty =
            DependencyProperty.Register("SecondaryColor", typeof(Color), typeof(StandardColorPicker),
                new PropertyMetadata(Colors.White));

        private readonly Image _colorPalette;


        private NotifyableColorRgba _notifyableColorRgba;
        private NotifyableColorHsv _notifyableColorHsv;


        public StandardColorPicker()
        {
            InitializeComponent();
            _colorPalette = FindName("colorPalette") as Image;
            NotifyableColorRgba = new NotifyableColorRgba(SelectedColor);
            NotifyableColorHsv = new NotifyableColorHsv(SelectedColor);
            NotifyableColorRgba.ColorChanged += OnNotifyableColorRgbChange;
            NotifyableColorHsv.ColorChanged += OnNotifyableColorHsvChange;
        }

        public NotifyableColorRgba NotifyableColorRgba
        {
            get => _notifyableColorRgba;
            set
            {
                _notifyableColorRgba = value;
                RaisePropertyChanged("NotifyableColorRgba");
            }
        }

        public NotifyableColorHsv NotifyableColorHsv
        {
            get => _notifyableColorHsv;
            set
            {
                _notifyableColorHsv = value;
                RaisePropertyChanged("NotifyableColorHsv");
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

        private void OnNotifyableColorRgbChange(object sender, EventArgs e)
        {
            SelectedColor = Color.FromArgb((byte)(NotifyableColorRgba.A * 255), (byte)(NotifyableColorRgba.R * 255), (byte)(NotifyableColorRgba.G * 255), (byte)(NotifyableColorRgba.B * 255));
            NotifyableColorHsv.SetRgbQuietly(NotifyableColorRgba.R, NotifyableColorRgba.G, NotifyableColorRgba.B);
        }

        private void OnNotifyableColorHsvChange(object sender, EventArgs e)
        {
            var (r, g, b) = HsvHelper.HsvToRgb(NotifyableColorHsv.H, NotifyableColorHsv.S, NotifyableColorHsv.V);
            NotifyableColorRgba.SetRgbQuietly(r, g, b);
            SelectedColor = Color.FromArgb((byte)(NotifyableColorRgba.A * 255), (byte)(NotifyableColorRgba.R * 255), (byte)(NotifyableColorRgba.G * 255), (byte)(NotifyableColorRgba.B * 255));
        }

        private static void OnSelectedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Color color = (Color)e.NewValue;
            ((StandardColorPicker)d).NotifyableColorRgba.SetArgb(color.A, color.R, color.G, color.B);
        }

        private void SwapColors()
        {
            Color tmp = SecondaryColor;
            SecondaryColor = SelectedColor;
            SelectedColor = tmp;
        }

        private void CalculateColor(Point pos)
        {
            pos.X = Math.Clamp(pos.X, 0, _colorPalette.ActualWidth);
            pos.Y = Math.Abs(Math.Clamp(pos.Y, 0, _colorPalette.ActualHeight) - _colorPalette.ActualHeight);
            int h = (int) (pos.X * 360f / _colorPalette.ActualWidth);
            float l = (float) (pos.Y * 100f / _colorPalette.ActualHeight);

            SelectedColor = HslHelper.HslToRGB(h, 100, l);
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
            PickColor((Image)sender, e);
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

            PickColor((Image)sender, e);
        }

        private void PickColor(Image palettte, MouseEventArgs e)
        {
            Point point = e.GetPosition(palettte);
            CalculateColor(point);
        }
    }
}