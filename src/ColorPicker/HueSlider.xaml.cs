using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorPicker
{
    public partial class HueSlider : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(HueSlider), 
                new PropertyMetadata(0.0));

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set
            {
                SetValue(ValueProperty, value);
                RaisePropertyChanged(nameof(Value));
            }
        }
        public HueSlider()
        {
            InitializeComponent();
        }
        private void RaisePropertyChanged(string property)
        {
            if (property != null) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ((UIElement)sender).CaptureMouse();
            Path circle = (Path)sender;
            Point mousePos = e.GetPosition(circle);
            UpdateValue(mousePos, circle.ActualWidth, circle.ActualHeight);
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            ((UIElement)sender).ReleaseMouseCapture();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!((UIElement)sender).IsMouseCaptured)
                return;
            Path circle = (Path)sender;
            Point mousePos = e.GetPosition(circle);
            UpdateValue(mousePos, circle.ActualWidth, circle.ActualHeight);
        }

        private void UpdateValue(Point mousePos, double width, double height)
        {
            double x = mousePos.X / (width * 2);
            double y = mousePos.Y / (height * 2);

            double length = Math.Sqrt(x * x + y * y);
            if (length == 0)
                return;
            double angle = Math.Acos(x / length);
            if (y < 0)
                angle = -angle;
            angle = angle * 360 / (Math.PI * 2) + 180;
            Value = Math.Clamp(angle, 0, 360);
        }
    }
}
