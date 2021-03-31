namespace ColorPicker.Models
{
    public struct ColorState
    {
        double _RGB_R;
        double _RGB_G;
        double _RGB_B;
        double _A;
        double _HSV_H;
        double _HSV_S;
        double _HSV_V;

        public ColorState(double rGB_R, double rGB_G, double rGB_B, double a, double hSV_H, double hSV_S, double hSV_V)
        {
            _RGB_R = rGB_R;
            _RGB_G = rGB_G;
            _RGB_B = rGB_B;
            _A = a;
            _HSV_H = hSV_H;
            _HSV_S = hSV_S;
            _HSV_V = hSV_V;
        }

        public void SetARGB(double a, double r, double g, double b)
        {
            _A = a;
            _RGB_R = r;
            _RGB_G = g;
            _RGB_B = b;
            RecalculateHSV();
        }
        public double A
        {
            get => _A;
            set
            {
                _A = value;
            }
        }

        public double RGB_R
        {
            get => _RGB_R;
            set
            {
                _RGB_R = value;
                RecalculateHSV();
            }
        }

        public double RGB_G
        {
            get => _RGB_G;
            set
            {
                _RGB_G = value;
                RecalculateHSV();
            }
        }

        public double RGB_B
        {
            get => _RGB_B;
            set
            {
                _RGB_B = value;
                RecalculateHSV();
            }
        }

        public double HSV_H
        {
            get => _HSV_H;
            set
            {
                _HSV_H = value;
                RecalculateRGB();
            }
        }

        public double HSV_S
        {
            get => _HSV_S;
            set
            {
                _HSV_S = value;
                RecalculateRGB();
            }
        }

        public double HSV_V
        {
            get => _HSV_V;
            set
            {
                _HSV_V = value;
                RecalculateRGB();
            }
        }

        private void RecalculateHSV()
        {
            var hsvtuple = ColorSpaceHelper.RgbToHsv(_RGB_R, _RGB_G, _RGB_B);
            double h = hsvtuple.Item1, s = hsvtuple.Item2, v = hsvtuple.Item3;
            if (h != -1)
                _HSV_H = h;
            if (s != -1)
                _HSV_S = s;
            _HSV_V = v;
        }

        private void RecalculateRGB()
        {
            var rgbtuple = ColorSpaceHelper.HsvToRgb(_HSV_H, _HSV_S, _HSV_V);
            _RGB_R = rgbtuple.Item1;
            _RGB_G = rgbtuple.Item2;
            _RGB_B = rgbtuple.Item3;
        }
    }
}
