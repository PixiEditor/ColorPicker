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
            double h = hsltuple.H, s = hsltuple.S, l = hsltuple.L;
            if (h != -1)
                _HSL_H = h;
            if (s != -1)
                _HSL_S = s;
            _HSL_L = l;
        }

        private void RecalculateHSLFromHSV()
        {
            var hsl = HslHelper.HsvToHsl(_HSV_H, _HSV_S, _HSV_V);
            _HSL_H = hsl.H;
            
            double s = hsl.S;
            if (s != -1)
                _HSL_S = s;
            
            _HSL_L = HSL_L;
        }
        
        private void RecalculateHSLFromOKHSV()
        {
            var hsl = HslHelper.OkHsvToHsl(_OKHSV_H, _OKHSV_S, _OKHSV_V);
            _HSL_H = hsl.H;
            _HSL_S = hsl.S;// todo add -1 checks if necessary
            _HSL_L = hsl.L;
        }
        
        private void RecalculateHSLFromOKHSL()
        {
            var hsl = HslHelper.OkHslToHsl(_OKHSL_H, _OKHSL_S, _OKHSL_L);
            _HSL_H = hsl.H;
            _HSL_S = hsl.S;// todo add -1 checks if necessary
            _HSL_L = hsl.L;
        }
        
        private void RecalculateHSVFromRGB()
        {
            var hsv = HsvHelper.RgbToHsv(_RGB_R, _RGB_G, _RGB_B);
            double h = hsv.H, s = hsv.S;
            if (h != -1)
                _HSV_H = h;
            if (s != -1)
                _HSV_S = s;
            _HSV_V = hsv.V;
        }

        private void RecalculateHSVFromHSL()
        {
            var hsv = HsvHelper.HslToHsv(_HSL_H, _HSL_S, _HSL_L);
            _HSV_H = hsv.H;

            double s = hsv.S;
            if (s != -1)
                _HSV_S = s;
            
            _HSV_V = hsv.V;
        }
        
        private void RecalculateHSVFromOKHSV()
        {
            var hsv = HsvHelper.OkHsvToHsv(_OKHSV_H, _OKHSV_S, _OKHSV_V);
            _HSV_H = hsv.H;
            _HSV_S = hsv.S;// todo add -1 checks if necessary
            _HSV_V = hsv.V;
        }
        
        private void RecalculateHSVFromOKHSL()
        {
            var hsv = HsvHelper.OkHslToHsv(_OKHSL_H, _OKHSL_S, _OKHSL_L);
            _HSV_H = hsv.H;
            _HSV_S = hsv.S;// todo add -1 checks if necessary
            _HSV_V = hsv.V;
        }
        
        
        

        private void RecalculateRGBFromHSL()
        {
            var rgb = RgbHelper.HslToRgb(_HSL_H, _HSL_S, _HSL_L);
            _RGB_R = rgb.R;
            _RGB_G = rgb.G;
            _RGB_B = rgb.B;
        }

        private void RecalculateRGBFromHSV()
        {
            var rgb = RgbHelper.HsvToRgb(_HSV_H, _HSV_S, _HSV_V);
            _RGB_R = rgb.R;
            _RGB_G = rgb.G;
            _RGB_B = rgb.B;
        }
        
        private void RecalculateRGBFromOKHSL()
        {
            var rgb = RgbHelper.OkHslToRgb(_OKHSL_H, _OKHSL_S, _OKHSL_L);
            _RGB_R = rgb.R;
            _RGB_G = rgb.G;// todo add -1 checks if necessary
            _RGB_B = rgb.B;
        }

        private void RecalculateRGBFromOKHSV()
        {
            var rgb = RgbHelper.OkHsvToRgb(_OKHSV_H, _OKHSV_S, _OKHSV_V);
            _RGB_R = rgb.R;
            _RGB_G = rgb.G;// todo add -1 checks if necessary
            _RGB_B = rgb.B;
        }
        
        
        
        
        private void RecalculateOKHSLFromRGB()
        {
            var okHsl = OkHslHelper.RgbToOkHsl(_RGB_R, _RGB_G, _RGB_B);
            _OKHSL_H = okHsl.H;
            _OKHSL_S = okHsl.S;// todo add -1 checks if necessary
            _OKHSL_L = okHsl.L;
        }
        
        private void RecalculateOKHSLFromHSL()
        {
            var okHsl = OkHslHelper.HslToOkHsl(_HSL_H, _HSL_S, _HSL_L);
            _OKHSL_H = okHsl.H;
            _OKHSL_S = okHsl.S;// todo add -1 checks if necessary
            _OKHSL_L = okHsl.L;
        }

        private void RecalculateOKHSLFromHSV()
        {
            var okHsl = OkHslHelper.HsvToOkHsl(_HSV_H, _HSV_S, _HSV_V);
            _OKHSL_H = okHsl.H;
            _OKHSL_S = okHsl.S;// todo add -1 checks if necessary
            _OKHSL_L = okHsl.L;
        }
        
        private void RecalculateOKHSLFromOKHSV()
        {
            var okHsl = OkHslHelper.OkHsvToOkHsl(_OKHSV_H, _OKHSV_S, _OKHSV_V);
            _OKHSL_H = okHsl.H;
            _OKHSL_S = okHsl.S;// todo add -1 checks if necessary
            _OKHSL_L = okHsl.L;
        }
        
        private void RecalculateOKHSVFromRGB()
        {
            var okHsv = OkHsvHelper.RgbToOkHsv(_RGB_R, _RGB_G, _RGB_B);
            _OKHSV_H = okHsv.H;
            _OKHSV_S = okHsv.S;// todo add -1 checks if necessary
            _OKHSV_V = okHsv.V;
        }
        
        private void RecalculateOKHSVFromHSL()
        {
            var okHsv = OkHsvHelper.HslToOkHsv(_HSL_H, _HSL_S, _HSL_L);
            _OKHSV_H = okHsv.H;
            _OKHSV_S = okHsv.S;// todo add -1 checks if necessary
            _OKHSV_V = okHsv.V;
        }

        private void RecalculateOKHSVFromHSV()
        {
            var okHsv = OkHsvHelper.HsvToOkHsv(_HSV_H, _HSV_S, _HSV_V);
            _OKHSV_H = okHsv.H;
            _OKHSV_S = okHsv.S;// todo add -1 checks if necessary
            _OKHSV_V = okHsv.V;
        }
        
        private void RecalculateOKHSVFromOKHSL()
        {
            var okhsvtuple = OkHsvHelper.OkHslToOkHsv(_OKHSL_H, _OKHSL_S, _OKHSL_L);
            _OKHSV_H = okhsvtuple.H;
            _OKHSV_S = okhsvtuple.S;// todo add -1 checks if necessary
            _OKHSV_V = okhsvtuple.V;
        }
    }
}