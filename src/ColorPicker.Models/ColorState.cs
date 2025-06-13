using System;

namespace ColorPicker.Models
{
    public struct ColorState : IEquatable<ColorState>
    {
        private double _RGB_R;
        private double _RGB_G;
        private double _RGB_B;

        private double _HSV_H;
        private double _HSV_S;
        private double _HSV_V;

        private double _HSL_H;
        private double _HSL_S;
        private double _HSL_L;

        public ColorState(double rGB_R, double rGB_G, double rGB_B, double a, double hSV_H, double hSV_S, double hSV_V,
            double hSL_h, double hSL_s, double hSL_l)
        {
            _RGB_R = rGB_R;
            _RGB_G = rGB_G;
            _RGB_B = rGB_B;
            A = a;
            _HSV_H = hSV_H;
            _HSV_S = hSV_S;
            _HSV_V = hSV_V;
            _HSL_H = hSL_h;
            _HSL_S = hSL_s;
            _HSL_L = hSL_l;
        }

        public void SetARGB(double a, double r, double g, double b)
        {
            A = a;
            _RGB_R = r;
            _RGB_G = g;
            _RGB_B = b;
            RecalculateHSVFromRGB();
            RecalculateHSLFromRGB();
        }

        public double A { get; set; }

        public double RGB_R
        {
            get => _RGB_R;
            set
            {
                _RGB_R = value;
                RecalculateHSVFromRGB();
                RecalculateHSLFromRGB();
            }
        }

        public double RGB_G
        {
            get => _RGB_G;
            set
            {
                _RGB_G = value;
                RecalculateHSVFromRGB();
                RecalculateHSLFromRGB();
            }
        }

        public double RGB_B
        {
            get => _RGB_B;
            set
            {
                _RGB_B = value;
                RecalculateHSVFromRGB();
                RecalculateHSLFromRGB();
            }
        }

        public double HSV_H
        {
            get => _HSV_H;
            set
            {
                _HSV_H = value;
                RecalculateRGBFromHSV();
                RecalculateHSLFromHSV();
            }
        }

        public double HSV_S
        {
            get => _HSV_S;
            set
            {
                _HSV_S = value;
                RecalculateRGBFromHSV();
                RecalculateHSLFromHSV();
            }
        }

        public double HSV_V
        {
            get => _HSV_V;
            set
            {
                _HSV_V = value;
                RecalculateRGBFromHSV();
                RecalculateHSLFromHSV();
            }
        }

        public double HSL_H
        {
            get => _HSL_H;
            set
            {
                _HSL_H = value;
                RecalculateRGBFromHSL();
                RecalculateHSVFromHSL();
            }
        }

        public double HSL_S
        {
            get => _HSL_S;
            set
            {
                _HSL_S = value;
                RecalculateRGBFromHSL();
                RecalculateHSVFromHSL();
            }
        }

        public double HSL_L
        {
            get => _HSL_L;
            set
            {
                _HSL_L = value;
                RecalculateRGBFromHSL();
                RecalculateHSVFromHSL();
            }
        }

        private void RecalculateHSLFromRGB()
        {
            var hsltuple = ColorSpaceHelper.RgbToHsl(_RGB_R, _RGB_G, _RGB_B);
            double h = hsltuple.Item1, s = hsltuple.Item2, l = hsltuple.Item3;
            if (h != -1)
                _HSL_H = h;
            if (s != -1)
                _HSL_S = s;
            _HSL_L = l;
        }

        private void RecalculateHSLFromHSV()
        {
            var hsltuple = ColorSpaceHelper.HsvToHsl(_HSV_H, _HSV_S, _HSV_V);
            double h = hsltuple.Item1, s = hsltuple.Item2, l = hsltuple.Item3;
            _HSL_H = h;
            if (s != -1)
                _HSL_S = s;
            _HSL_L = l;
        }

        private void RecalculateHSVFromRGB()
        {
            var hsvtuple = ColorSpaceHelper.RgbToHsv(_RGB_R, _RGB_G, _RGB_B);
            double h = hsvtuple.Item1, s = hsvtuple.Item2, v = hsvtuple.Item3;
            if (h != -1)
                _HSV_H = h;
            if (s != -1)
                _HSV_S = s;
            _HSV_V = v;
        }

        private void RecalculateHSVFromHSL()
        {
            var hsvtuple = ColorSpaceHelper.HslToHsv(_HSL_H, _HSL_S, _HSL_L);
            double h = hsvtuple.Item1, s = hsvtuple.Item2, v = hsvtuple.Item3;
            _HSV_H = h;
            if (s != -1)
                _HSV_S = s;
            _HSV_V = v;
        }

        private void RecalculateRGBFromHSL()
        {
            var rgbtuple = ColorSpaceHelper.HslToRgb(_HSL_H, _HSL_S, _HSL_L);
            _RGB_R = rgbtuple.Item1;
            _RGB_G = rgbtuple.Item2;
            _RGB_B = rgbtuple.Item3;
        }

        private void RecalculateRGBFromHSV()
        {
            var rgbtuple = ColorSpaceHelper.HsvToRgb(_HSV_H, _HSV_S, _HSV_V);
            _RGB_R = rgbtuple.Item1;
            _RGB_G = rgbtuple.Item2;
            _RGB_B = rgbtuple.Item3;
        }

        public static ColorState Lerp(ColorState from, ColorState to, double t)
        {
            return new ColorState(
                from.RGB_R + (to.RGB_R - from.RGB_R) * t,
                from.RGB_G + (to.RGB_G - from.RGB_G) * t,
                from.RGB_B + (to.RGB_B - from.RGB_B) * t,
                from.A + (to.A - from.A) * t,
                from.HSV_H + (to.HSV_H - from.HSV_H) * t,
                from.HSV_S + (to.HSV_S - from.HSV_S) * t,
                from.HSV_V + (to.HSV_V - from.HSV_V) * t,
                from.HSL_H + (to.HSL_H - from.HSL_H) * t,
                from.HSL_S + (to.HSL_S - from.HSL_S) * t,
                from.HSL_L + (to.HSL_L - from.HSL_L) * t
            );
        }

        public bool Equals(ColorState other)
        {
            return _RGB_R.Equals(other._RGB_R) && _RGB_G.Equals(other._RGB_G) && _RGB_B.Equals(other._RGB_B) &&
                   _HSV_H.Equals(other._HSV_H) && _HSV_S.Equals(other._HSV_S) && _HSV_V.Equals(other._HSV_V) &&
                   _HSL_H.Equals(other._HSL_H) && _HSL_S.Equals(other._HSL_S) && _HSL_L.Equals(other._HSL_L) &&
                   A.Equals(other.A);
        }

        public override bool Equals(object obj)
        {
            return obj is ColorState other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _RGB_R.GetHashCode();
                hashCode = (hashCode * 397) ^ _RGB_G.GetHashCode();
                hashCode = (hashCode * 397) ^ _RGB_B.GetHashCode();
                hashCode = (hashCode * 397) ^ _HSV_H.GetHashCode();
                hashCode = (hashCode * 397) ^ _HSV_S.GetHashCode();
                hashCode = (hashCode * 397) ^ _HSV_V.GetHashCode();
                hashCode = (hashCode * 397) ^ _HSL_H.GetHashCode();
                hashCode = (hashCode * 397) ^ _HSL_S.GetHashCode();
                hashCode = (hashCode * 397) ^ _HSL_L.GetHashCode();
                hashCode = (hashCode * 397) ^ A.GetHashCode();
                return hashCode;
            }
        }
    }
}