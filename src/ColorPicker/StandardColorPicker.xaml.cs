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
            DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(StandardColorPicker),
                new PropertyMetadata(Colors.Black, OnSelectedColorChanged));

        public static readonly DependencyProperty SecondaryColorProperty =
            DependencyProperty.Register(nameof(SecondaryColor), typeof(Color), typeof(StandardColorPicker),
                new PropertyMetadata(Colors.White));

        public static readonly DependencyProperty HueSmallChangeProperty =
            DependencyProperty.Register(nameof(HueSmallChange), typeof(double), typeof(StandardColorPicker),
                new PropertyMetadata(1.0));

        public static readonly DependencyProperty SmallChangeProperty =
            DependencyProperty.Register(nameof(SmallChange), typeof(double), typeof(StandardColorPicker),
                new PropertyMetadata(0.00390625));

        private NotifyableColorRgba _notifyableColorRgba;
        private NotifyableColorHsv _notifyableColorHsv;


        public StandardColorPicker()
        {
            InitializeComponent();
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
                RaisePropertyChanged(nameof(NotifyableColorRgba));
            }
        }

        public NotifyableColorHsv NotifyableColorHsv
        {
            get => _notifyableColorHsv;
            set
            {
                _notifyableColorHsv = value;
                RaisePropertyChanged(nameof(NotifyableColorHsv));
            }
        }

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

        private void RaisePropertyChanged(string property)
        {
            if (property != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SwapColors();
        }
    }
}