using ColorPicker.Models.ColorSpaces;

namespace ColorPicker.Models
{
    public struct ColorState
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
        
        private double _OKHSV_H;
        private double _OKHSV_S;
        private double _OKHSV_V;

        private double _OKHSL_H;
        private double _OKHSL_S;
        private double _OKHSL_L;

        public ColorState(
            double rGB_R, double rGB_G, double rGB_B, double a, 
            double hSV_H, double hSV_S, double hSV_V,
            double hSL_h, double hSL_s, double hSL_l,
            double oKHSV_H, double oKHSV_S, double oKHSV_V,
            double oKHSL_H, double oKHSL_S, double oKHSL_L)
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

            _OKHSV_H = oKHSV_H;
            _OKHSV_S = oKHSV_S;
            _OKHSV_V = oKHSV_V;
            _OKHSL_H = oKHSL_H;
            _OKHSL_S = oKHSL_S; 
            _OKHSL_L = oKHSL_L;
        }

        public void SetARGB(double a, double r, double g, double b)
        {
            A = a;
            _RGB_R = r;
            _RGB_G = g;
            _RGB_B = b;
            RecalculateHSVFromRGB();
            RecalculateHSLFromRGB();
            RecalculateOKHSLFromRGB();
            RecalculateOKHSVFromRGB();
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
                RecalculateOKHSLFromRGB();
                RecalculateOKHSVFromRGB();
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
                RecalculateOKHSLFromRGB();
                RecalculateOKHSVFromRGB();
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
                RecalculateOKHSLFromRGB();
                RecalculateOKHSVFromRGB();
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
                RecalculateOKHSLFromHSV();
                RecalculateOKHSVFromHSV();
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
                RecalculateOKHSLFromHSV();
                RecalculateOKHSVFromHSV();
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
                RecalculateOKHSLFromHSV();
                RecalculateOKHSVFromHSV();
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
                RecalculateOKHSLFromHSL();
                RecalculateOKHSVFromHSL();
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
                RecalculateOKHSLFromHSL();
                RecalculateOKHSVFromHSL();
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
                RecalculateOKHSLFromHSL();
                RecalculateOKHSVFromHSL();
            }
        }
        
        public double OKHSV_H
        {
            get => _OKHSV_H;
            set
            {
                _OKHSV_H = value;
                RecalculateRGBFromOKHSV();
                RecalculateHSLFromOKHSV();
                RecalculateHSVFromOKHSV();
                RecalculateOKHSLFromOKHSV();
            }
        }

        public double OKHSV_S
        {
            get => _OKHSV_S;
            set
            {
                _OKHSV_S = value;
                RecalculateRGBFromOKHSV();
                RecalculateHSLFromOKHSV();
                RecalculateHSVFromOKHSV();
                RecalculateOKHSLFromOKHSV();
            }
        }

        public double OKHSV_V
        {
            get => _OKHSV_V;
            set
            {
                _OKHSV_V = value;
                RecalculateRGBFromOKHSV();
                RecalculateHSLFromOKHSV();
                RecalculateHSVFromOKHSV();
                RecalculateOKHSLFromOKHSV();
            }
        }

        public double OKHSL_H
        {
            get => _OKHSL_H;
            set
            {
                _OKHSL_H = value;
                RecalculateRGBFromOKHSL();
                RecalculateHSLFromOKHSL();
                RecalculateHSVFromOKHSL();
                RecalculateOKHSVFromOKHSL();
            }
        }

        public double OKHSL_S
        {
            get => _OKHSL_S;
            set
            {
                _OKHSL_S = value;
                RecalculateRGBFromOKHSL();
                RecalculateHSLFromOKHSL();
                RecalculateHSVFromOKHSL();
                RecalculateOKHSVFromOKHSL();
            }
        }

        public double OKHSL_L
        {
            get => _OKHSL_L;
            set
            {
                _OKHSL_L = value;
                RecalculateRGBFromOKHSL();
                RecalculateHSLFromOKHSL();
                RecalculateHSVFromOKHSL();
                RecalculateOKHSVFromOKHSL();
            }
        }
        
        
        

        private void RecalculateHSLFromRGB()
        {
            var hsltuple = HslHelper.RgbToHsl(_RGB_R, _RGB_G, _RGB_B);
            double h = hsltuple.Item1, s = hsltuple.Item2, l = hsltuple.Item3;
            if (h != -1)
                _HSL_H = h;
            if (s != -1)
                _HSL_S = s;
            _HSL_L = l;
        }

        private void RecalculateHSLFromHSV()
        {
            var hsltuple = HslHelper.HsvToHsl(_HSV_H, _HSV_S, _HSV_V);
            double h = hsltuple.Item1, s = hsltuple.Item2, l = hsltuple.Item3;
            _HSL_H = h;
            if (s != -1)
                _HSL_S = s;
            _HSL_L = l;
        }
        
        private void RecalculateHSLFromOKHSV()
        {
            var hsltuple = HslHelper.OkHsvToHsl(_OKHSV_H, _OKHSV_S, _OKHSV_V);
            double h = hsltuple.Item1, s = hsltuple.Item2, l = hsltuple.Item3;
            _HSL_H = h;
            _HSL_S = s;// todo add -1 checks if necessary
            _HSL_L = l;
        }
        
        private void RecalculateHSLFromOKHSL()
        {
            var hsltuple = HslHelper.OkHslToHsl(_OKHSL_H, _OKHSL_S, _OKHSL_L);
            double h = hsltuple.Item1, s = hsltuple.Item2, l = hsltuple.Item3;
            _HSL_H = h;
            _HSL_S = s;// todo add -1 checks if necessary
            _HSL_L = l;
        }
        
        
        

        private void RecalculateHSVFromRGB()
        {
            var hsvtuple = HsvHelper.RgbToHsv(_RGB_R, _RGB_G, _RGB_B);
            double h = hsvtuple.Item1, s = hsvtuple.Item2, v = hsvtuple.Item3;
            if (h != -1)
                _HSV_H = h;
            if (s != -1)
                _HSV_S = s;
            _HSV_V = v;
        }

        private void RecalculateHSVFromHSL()
        {
            var hsvtuple = HsvHelper.HslToHsv(_HSL_H, _HSL_S, _HSL_L);
            double h = hsvtuple.Item1, s = hsvtuple.Item2, v = hsvtuple.Item3;
            _HSV_H = h;
            if (s != -1)
                _HSV_S = s;
            _HSV_V = v;
        }
        
        private void RecalculateHSVFromOKHSV()
        {
            var hsvtuple = HsvHelper.OkHsvToHsv(_OKHSV_H, _OKHSV_S, _OKHSV_V);
            double h = hsvtuple.Item1, s = hsvtuple.Item2, v = hsvtuple.Item3;
            _HSV_H = h;
            _HSV_S = s;// todo add -1 checks if necessary
            _HSV_V = v;
        }
        
        private void RecalculateHSVFromOKHSL()
        {
            var hsvtuple = HsvHelper.OkHslToHsv(_OKHSL_H, _OKHSL_S, _OKHSL_L);
            double h = hsvtuple.Item1, s = hsvtuple.Item2, v = hsvtuple.Item3;
            _HSV_H = h;
            _HSV_S = s;// todo add -1 checks if necessary
            _HSV_V = v;
        }
        
        
        

        private void RecalculateRGBFromHSL()
        {
            var rgbtuple = RgbHelper.HslToRgb(_HSL_H, _HSL_S, _HSL_L);
            _RGB_R = rgbtuple.Item1;
            _RGB_G = rgbtuple.Item2;
            _RGB_B = rgbtuple.Item3;
        }

        private void RecalculateRGBFromHSV()
        {
            var rgbtuple = RgbHelper.HsvToRgb(_HSV_H, _HSV_S, _HSV_V);
            _RGB_R = rgbtuple.Item1;
            _RGB_G = rgbtuple.Item2;
            _RGB_B = rgbtuple.Item3;
        }
        
        private void RecalculateRGBFromOKHSL()
        {
            var rgbtuple = RgbHelper.OkHslToRgb(_OKHSL_H, _OKHSL_S, _OKHSL_L);
            _RGB_R = rgbtuple.Item1;
            _RGB_G = rgbtuple.Item2;// todo add -1 checks if necessary
            _RGB_B = rgbtuple.Item3;
        }

        private void RecalculateRGBFromOKHSV()
        {
            var rgbtuple = RgbHelper.OkHsvToRgb(_OKHSV_H, _OKHSV_S, _OKHSV_V);
            _RGB_R = rgbtuple.Item1;
            _RGB_G = rgbtuple.Item2;// todo add -1 checks if necessary
            _RGB_B = rgbtuple.Item3;
        }
        
        
        
        
        private void RecalculateOKHSLFromRGB()
        {
            var okhsltuple = OkHslHelper.RgbToOkHsl(_RGB_R, _RGB_G, _RGB_B);
            _OKHSL_H = okhsltuple.Item1;
            _OKHSL_S = okhsltuple.Item2;// todo add -1 checks if necessary
            _OKHSL_L = okhsltuple.Item3;
        }
        
        private void RecalculateOKHSLFromHSL()
        {
            var okhsltuple = OkHslHelper.HslToOkHsl(_HSL_H, _HSL_S, _HSL_L);
            _OKHSL_H = okhsltuple.Item1;
            _OKHSL_S = okhsltuple.Item2;// todo add -1 checks if necessary
            _OKHSL_L = okhsltuple.Item3;
        }

        private void RecalculateOKHSLFromHSV()
        {
            var okhsltuple = OkHslHelper.HsvToOkHsl(_HSV_H, _HSV_S, _HSV_V);
            _OKHSL_H = okhsltuple.Item1;
            _OKHSL_S = okhsltuple.Item2;// todo add -1 checks if necessary
            _OKHSL_L = okhsltuple.Item3;
        }
        
        private void RecalculateOKHSLFromOKHSV()
        {
            var okhsltuple = OkHslHelper.OkHsvToOkHsl(_OKHSV_H, _OKHSV_S, _OKHSV_V);
            _OKHSL_H = okhsltuple.Item1;
            _OKHSL_S = okhsltuple.Item2;// todo add -1 checks if necessary
            _OKHSL_L = okhsltuple.Item3;
        }
        
        
        
        
        private void RecalculateOKHSVFromRGB()
        {
            var okhsvtuple = OkHsvHelper.RgbToOkHsv(_RGB_R, _RGB_G, _RGB_B);
            _OKHSV_H = okhsvtuple.Item1;
            _OKHSV_S = okhsvtuple.Item2;// todo add -1 checks if necessary
            _OKHSV_V = okhsvtuple.Item3;
        }
        
        private void RecalculateOKHSVFromHSL()
        {
            var okhsvtuple = OkHsvHelper.HslToOkHsv(_HSL_H, _HSL_S, _HSL_L);
            _OKHSV_H = okhsvtuple.Item1;
            _OKHSV_S = okhsvtuple.Item2;// todo add -1 checks if necessary
            _OKHSV_V = okhsvtuple.Item3;
        }

        private void RecalculateOKHSVFromHSV()
        {
            var okhsvtuple = OkHsvHelper.HsvToOkHsv(_HSV_H, _HSV_S, _HSV_V);
            _OKHSV_H = okhsvtuple.Item1;
            _OKHSV_S = okhsvtuple.Item2;// todo add -1 checks if necessary
            _OKHSV_V = okhsvtuple.Item3;
        }
        
        private void RecalculateOKHSVFromOKHSL()
        {
            var okhsvtuple = OkHsvHelper.OkHslToOkHsv(_OKHSL_H, _OKHSL_S, _OKHSL_L);
            _OKHSV_H = okhsvtuple.Item1;
            _OKHSV_S = okhsvtuple.Item2;// todo add -1 checks if necessary
            _OKHSV_V = okhsvtuple.Item3;
        }
    }
}