using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ColorPicker.Models
{
    public class NotifyableColorRgba : NotifyableObject
    {
        public double A
        {
            get => _a;
            set
            {
                if (value == _a)
                    return;
                _a = value;
                ColorChanged?.Invoke(this, EventArgs.Empty);
                RaisePropertyChanged("A");
            }
        }

        public double R
        {
            get => _r;
            set
            {
                if (value == _r)
                    return;
                _r = value;
                ColorChanged?.Invoke(this, EventArgs.Empty);
                RaisePropertyChanged("R");
            }
        }

        public double G
        {
            get => _g;
            set
            {
                if (value == _g)
                    return;
                _g = value;
                ColorChanged?.Invoke(this, EventArgs.Empty);
                RaisePropertyChanged("G");
            }
        }

        public double B
        {
            get => _b;
            set
            {
                if (value == _b)
                    return;
                _b = value;
                ColorChanged?.Invoke(this, EventArgs.Empty);
                RaisePropertyChanged("B");
            }
        }

        private double _a;
        private double _r;
        private double _g;
        private double _b;

        public NotifyableColorRgba(Color color)
        {
            A = color.A / 255.0;
            R = color.R / 255.0;
            G = color.G / 255.0;
            B = color.B / 255.0;
        }

        public NotifyableColorRgba() { }

        public event EventHandler ColorChanged;
        
        public void SetArgb(byte a, byte r, byte g, byte b)
        {
            if ((byte)(A*255) != a)
                A = a / 255.0;
            if ((byte)(R * 255) != r)
                R = r / 255.0;
            if ((byte)(G * 255) != g)
                G = g / 255.0;
            if ((byte)(B * 255) != b)
                B = b / 255.0;
        }

        public void SetRgbQuietly(double r, double g, double b)
        {
            _r = r;
            RaisePropertyChanged("R");
            _g = g;
            RaisePropertyChanged("G");
            _b = b;
            RaisePropertyChanged("B");
        }
    }
}
