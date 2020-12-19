using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ColorPicker.Models
{
    public class NotifyableColorHsv : NotifyableObject
    {
        public double H
        {
            get => _h;
            set
            {
                if (value == -1)
                    return;
                _h = value;
                ColorChanged?.Invoke(this, EventArgs.Empty);
                RaisePropertyChanged("H");
            }
        }

        public double S
        {
            get => _s;
            set
            {
                if (value == -1)
                    return;
                _s = value;
                ColorChanged?.Invoke(this, EventArgs.Empty);
                RaisePropertyChanged("S");
            }
        }

        public double V
        {
            get => _v;
            set
            {
                _v = value;
                ColorChanged?.Invoke(this, EventArgs.Empty);
                RaisePropertyChanged("V");
            }
        }

        public event EventHandler ColorChanged;

        private double _h;
        private double _s;
        private double _v;

        public NotifyableColorHsv(Color color)
        {
            (_h, _s, _v) = HsvHelper.RgbToHsv(color.R/255.0, color.G / 255.0, color.B / 255.0);
            if (_h == -1)
                _h = 0;
            if (_s == -1)
                _s = 0;
        }

        public void SetRgbQuietly(double r, double g, double b)
        {
            var (curH, curS, curV) = HsvHelper.RgbToHsv(r, g, b);
            if (curH != -1)
            {
                _h = curH;
                RaisePropertyChanged("H");
            }
            if (curS != -1)
            {
                _s = curS;
                RaisePropertyChanged("S");
            }
            _v = curV;
            RaisePropertyChanged("V");
        }
    }
}
