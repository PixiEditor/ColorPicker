using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ColorPicker.Models
{
    public class NotifyableColor : NotifyableObject
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
                RaisePropertyChanged("H");
                RaisePropertyChanged("S");
                RaisePropertyChanged("V");
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
                RaisePropertyChanged("H");
                RaisePropertyChanged("S");
                RaisePropertyChanged("V");
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
                RaisePropertyChanged("H");
                RaisePropertyChanged("S");
                RaisePropertyChanged("V");
            }
        }

        public double H
        {
            get => HsvHelper.RgbToHsv(_r, _g, _b).Item1;
            set
            {
                var (curH, curS, curV) = HsvHelper.RgbToHsv(_r, _g, _b);
                (_r, _g, _b) = HsvHelper.HsvToRgb(value, curS, curV);
                ColorChanged?.Invoke(this, EventArgs.Empty);
                RaisePropertyChanged("R");
                RaisePropertyChanged("G");
                RaisePropertyChanged("B");
                RaisePropertyChanged("H");
            }
        }

        public double S
        {
            get => HsvHelper.RgbToHsv(_r, _g, _b).Item2;
            set
            {
                var (curH, curS, curV) = HsvHelper.RgbToHsv(_r, _g, _b);
                (_r, _g, _b) = HsvHelper.HsvToRgb(curH, value, curV);
                ColorChanged?.Invoke(this, EventArgs.Empty);
                RaisePropertyChanged("R");
                RaisePropertyChanged("G");
                RaisePropertyChanged("B");
                RaisePropertyChanged("S");
            }
        }

        public double V
        {
            get => HsvHelper.RgbToHsv(_r, _g, _b).Item3;
            set
            {
                var (curH, curS, curV) = HsvHelper.RgbToHsv(_r, _g, _b);
                (_r, _g, _b) = HsvHelper.HsvToRgb(curH, curS, value);
                ColorChanged?.Invoke(this, EventArgs.Empty);
                RaisePropertyChanged("R");
                RaisePropertyChanged("G");
                RaisePropertyChanged("B");
                RaisePropertyChanged("V");
            }
        }

        private double _a;

        private double _b;

        private double _g;

        private double _r;

        public NotifyableColor(Color color)
        {
            A = color.A / 255.0;
            R = color.R / 255.0;
            G = color.G / 255.0;
            B = color.B / 255.0;
        }

        public NotifyableColor() { }

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
    }
}
