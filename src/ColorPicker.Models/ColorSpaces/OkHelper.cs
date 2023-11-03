// Adapted from Björn Ottosson's C++ header https://bottosson.github.io/misc/ok_color.h
using System;
using ColorPicker.Models.Colors;

namespace ColorPicker.Models.ColorSpaces;

public static class OkHelper
{
	private struct LC
	{
		public double L;
		public double C;
	};

	/// <summary>
	/// Alternative representation of (L_cusp, C_cusp)
	/// Encoded so S = C_cusp/L_cusp and T = C_cusp/(1-L_cusp) 
	/// The maximum value for C in the triangle is then found as fmin(S*L, T*(1-L)), for a given L
	/// </summary> 
	private struct ST
	{
		public double S;
		public double T;
	};

	private static double SrgbTransferFunction(double a)
	{
		return .0031308 >= a ? 12.92 * a : 1.055 * Math.Pow(a, .4166666666666667) - .055;
	}

	private static double SrgbTransferFunctionInverse(double a)
	{
		return .04045 < a ? Math.Pow((a + .055) / 1.055, 2.4) : a / 12.92;
	}

	private static Lab LinearSrgbToOklab(Rgb c)
	{
		double l = 0.4122214708 * c.R + 0.5363325363 * c.G + 0.0514459929 * c.B;
		double m = 0.2119034982 * c.R + 0.6806995451 * c.G + 0.1073969566 * c.B;
		double s = 0.0883024619 * c.R + 0.2817188376 * c.G + 0.6299787005 * c.B;

		double l_ = Math.Cbrt(l);
		double m_ = Math.Cbrt(m);
		double s_ = Math.Cbrt(s);

		return new Lab(0.2104542553 * l_ + 0.7936177850 * m_ - 0.0040720468 * s_, 1.9779984951 * l_ - 2.4285922050 * m_ + 0.4505937099 * s_, 0.0259040371 * l_ + 0.7827717662 * m_ - 0.8086757660 * s_);
	}

	private static Rgb OklabToLinearSrgb(Lab c)
	{
		double l_ = c.L + 0.3963377774 * c.a + 0.2158037573 * c.b;
		double m_ = c.L - 0.1055613458 * c.a - 0.0638541728 * c.b;
		double s_ = c.L - 0.0894841775 * c.a - 1.2914855480 * c.b;

		double l = l_ * l_ * l_;
		double m = m_ * m_ * m_;
		double s = s_ * s_ * s_;

		return new Rgb(
			+4.0767416621 * l - 3.3077115913 * m + 0.2309699292 * s,
			-1.2684380046 * l + 2.6097574011 * m - 0.3413193965 * s,
			-0.0041960863 * l - 0.7034186147 * m + 1.7076147010 * s);
	}
		
	/// <summary>
	/// Finds the maximum saturation possible for a given hue that fits in sRGB
	/// Saturation here is defined as S = C/L
	/// a and b must be normalized so a^2 + b^2 == 1
	/// </summary>
	private static double ComputeMaxSaturation(double a, double b)
	{
		// Max saturation will be when one of r, g or b goes below zero.

		// Select different coefficients depending on which component goes below zero first
		double k0, k1, k2, k3, k4, wl, wm, ws;

		if (-1.88170328 * a - 0.80936493 * b > 1)
		{
			// Red component
			k0 = +1.19086277;
			k1 = +1.76576728;
			k2 = +0.59662641;
			k3 = +0.75515197;
			k4 = +0.56771245;
			wl = +4.0767416621;
			wm = -3.3077115913;
			ws = +0.2309699292;
		}
		else if (1.81444104 * a - 1.19445276 * b > 1)
		{
			// Green component
			k0 = +0.73956515;
			k1 = -0.45954404;
			k2 = +0.08285427;
			k3 = +0.12541070;
			k4 = +0.14503204;
			wl = -1.2684380046;
			wm = +2.6097574011;
			ws = -0.3413193965;
		}
		else
		{
			// Blue component
			k0 = +1.35733652;
			k1 = -0.00915799;
			k2 = -1.15130210;
			k3 = -0.50559606;
			k4 = +0.00692167;
			wl = -0.0041960863;
			wm = -0.7034186147;
			ws = +1.7076147010;
		}

		// Approximate max saturation using a polynomial:
		double S = k0 + k1 * a + k2 * b + k3 * a * a + k4 * a * b;

		// Do one step Halley's method to get closer
		// this gives an error less than 10e6, except for some blue hues where the dS/dh is close to infinite
		// this should be sufficient for most applications, otherwise do two/three steps 

		double k_l = +0.3963377774 * a + 0.2158037573 * b;
		double k_m = -0.1055613458 * a - 0.0638541728 * b;
		double k_s = -0.0894841775 * a - 1.2914855480 * b;

		{
			double l_ = 1.0 + S * k_l;
			double m_ = 1.0 + S * k_m;
			double s_ = 1.0 + S * k_s;

			double l = l_ * l_ * l_;
			double m = m_ * m_ * m_;
			double s = s_ * s_ * s_;

			double l_dS = 3.0 * k_l * l_ * l_;
			double m_dS = 3.0 * k_m * m_ * m_;
			double s_dS = 3.0 * k_s * s_ * s_;

			double l_dS2 = 6.0 * k_l * k_l * l_;
			double m_dS2 = 6.0 * k_m * k_m * m_;
			double s_dS2 = 6.0 * k_s * k_s * s_;

			double f = wl * l + wm * m + ws * s;
			double f1 = wl * l_dS + wm * m_dS + ws * s_dS;
			double f2 = wl * l_dS2 + wm * m_dS2 + ws * s_dS2;

			S = S - f * f1 / (f1 * f1 - 0.5 * f * f2);
		}

		return S;
	}

	/// <summary>
	/// finds L_cusp and C_cusp for a given hue
	/// a and b must be normalized so a^2 + b^2 == 1
	/// </summary>
	private static LC FindCusp(double a, double b)
	{
		// First, find the maximum saturation (saturation S = C/L)
		double S_cusp = ComputeMaxSaturation(a, b);

		// Convert to linear sRGB to find the first point where at least one of r,g or b >= 1:
		Rgb rgb_at_max = OklabToLinearSrgb(new Lab(1, S_cusp * a, S_cusp * b));
			
		double L_cusp = Math.Cbrt(1.0 / Math.Max(Math.Max(rgb_at_max.R, rgb_at_max.G), rgb_at_max.B));
		double C_cusp = L_cusp * S_cusp;

		return new LC
		{
			L = L_cusp,
			C = C_cusp
		};
	}

	/// <summary>
	/// Finds intersection of the line defined by 
	/// L = L0 * (1 - t) + t * L1;
	/// C = t * C1;
	/// a and b must be normalized so a^2 + b^2 == 1
	/// </summary>
	private static double FindGamutIntersection(double a, double b, double L1, double C1, double L0, LC cusp)
	{
		// Find the intersection for upper and lower half seprately
		double t;
		if (((L1 - L0) * cusp.C - (cusp.L - L0) * C1) <= 0.0)
		{
			// Lower half

			t = cusp.C * L0 / (C1 * cusp.L + cusp.C * (L0 - L1));
		}
		else
		{
			// Upper half

			// First intersect with triangle
			t = cusp.C * (L0 - 1.0) / (C1 * (cusp.L - 1.0) + cusp.C * (L0 - L1));

			// Then one step Halley's method
			{
				double dL = L1 - L0;
				double dC = C1;

				double k_l = +0.3963377774 * a + 0.2158037573 * b;
				double k_m = -0.1055613458 * a - 0.0638541728 * b;
				double k_s = -0.0894841775 * a - 1.2914855480 * b;

				double l_dt = dL + dC * k_l;
				double m_dt = dL + dC * k_m;
				double s_dt = dL + dC * k_s;


				// If higher accuracy is required, 2 or 3 iterations of the following block can be used:
				{
					double L = L0 * (1.0 - t) + t * L1;
					double C = t * C1;

					double l_ = L + C * k_l;
					double m_ = L + C * k_m;
					double s_ = L + C * k_s;

					double l = l_ * l_ * l_;
					double m = m_ * m_ * m_;
					double s = s_ * s_ * s_;

					double ldt = 3 * l_dt * l_ * l_;
					double mdt = 3 * m_dt * m_ * m_;
					double sdt = 3 * s_dt * s_ * s_;

					double ldt2 = 6 * l_dt * l_dt * l_;
					double mdt2 = 6 * m_dt * m_dt * m_;
					double sdt2 = 6 * s_dt * s_dt * s_;

					double r = 4.0767416621 * l - 3.3077115913 * m + 0.2309699292 * s - 1;
					double r1 = 4.0767416621 * ldt - 3.3077115913 * mdt + 0.2309699292 * sdt;
					double r2 = 4.0767416621 * ldt2 - 3.3077115913 * mdt2 + 0.2309699292 * sdt2;

					double u_r = r1 / (r1 * r1 - 0.5 * r * r2);
					double t_r = -r * u_r;

					double g = -1.2684380046 * l + 2.6097574011 * m - 0.3413193965 * s - 1;
					double g1 = -1.2684380046 * ldt + 2.6097574011 * mdt - 0.3413193965 * sdt;
					double g2 = -1.2684380046 * ldt2 + 2.6097574011 * mdt2 - 0.3413193965 * sdt2;

					double u_g = g1 / (g1 * g1 - 0.5 * g * g2);
					double t_g = -g * u_g;

					double _b = -0.0041960863 * l - 0.7034186147 * m + 1.7076147010 * s - 1;
					double b1 = -0.0041960863 * ldt - 0.7034186147 * mdt + 1.7076147010 * sdt;
					double b2 = -0.0041960863 * ldt2 - 0.7034186147 * mdt2 + 1.7076147010 * sdt2;

					double u_b = b1 / (b1 * b1 - 0.5 * _b * b2);
					double t_b = -_b * u_b;

					t_r = u_r >= 0.0 ? t_r : float.MaxValue;
					t_g = u_g >= 0.0 ? t_g : float.MaxValue;
					t_b = u_b >= 0.0 ? t_b : float.MaxValue;

					t += Math.Min(t_r, Math.Min(t_g, t_b));
				}
			}
		}

		return t;
	}

	private static double FindGamutIntersection(double a, double b, double L1, double C1, double L0)
	{
		// Find the cusp of the gamut triangle
		LC cusp = FindCusp(a, b);

		return FindGamutIntersection(a, b, L1, C1, L0, cusp);
	}

	private static double Toe(double x)
	{
		const double k_1 = 0.206;
		const double k_2 = 0.03;
		const double k_3 = (1.0 + k_1) / (1.0 + k_2);
		return 0.5 * (k_3 * x - k_1 + Math.Sqrt((k_3 * x - k_1) * (k_3 * x - k_1) + 4 * k_2 * k_3 * x));
	}

	private static double ToeInverse(double x)
	{
		const double k_1 = 0.206;
		const double k_2 = 0.03;
		const double k_3 = (1.0 + k_1) / (1.0 + k_2);
		return (x * x + k_1 * x) / (k_3 * (x + k_2));
	}

	private static ST ToSt(LC cusp)
	{
		double L = cusp.L;
		double C = cusp.C;
		return new ST()
		{
			S = C / L,
			T = C / (1 - L)
		};
	}

	/// <summary>
	/// Returns a smooth approximation of the location of the cusp
	/// This polynomial was created by an optimization process
	/// It has been designed so that S_mid < S_max and T_mid < T_max
	/// </summary>
	private static ST GetStMid(double a_, double b_)
	{
		double S = 0.11516993 + 1.0 / (
			+7.44778970 + 4.15901240 * b_
			            + a_ * (-2.19557347 + 1.75198401 * b_
			                                + a_ * (-2.13704948 - 10.02301043 * b_
			                                        + a_ * (-4.24894561 + 5.38770819 * b_ + 4.69891013 * a_
			                                        )))
		);

		double T = 0.11239642 + 1.0 / (
			+1.61320320 - 0.68124379 * b_
			+ a_ * (+0.40370612 + 0.90148123 * b_
			                    + a_ * (-0.27087943 + 0.61223990 * b_
			                                        + a_ * (+0.00299215 - 0.45399568 * b_ - 0.14661872 * a_
			                                        )))
		);

		return new ST()
		{
			S = S, 
			T = T
		};
	}

	struct Cs
	{
		public double C_0;
		public double C_mid;
		public double C_max;
	};

	private static Cs GetCs(double L, double a_, double b_)
	{
		LC cusp = FindCusp(a_, b_);

		double C_max = FindGamutIntersection(a_, b_, L, 1, L, cusp);
		ST ST_max = ToSt(cusp);

		// Scale factor to compensate for the curved part of gamut shape:
		double k = C_max / Math.Min((L * ST_max.S), (1 - L) * ST_max.T);

		double C_mid;
		{
			ST ST_mid = GetStMid(a_, b_);

			// Use a soft minimum function, instead of a sharp triangle shape to get a smooth value for chroma.
			double C_a = L * ST_mid.S;
			double C_b = (1.0 - L) * ST_mid.T;
			C_mid = 0.9 * k * Math.Sqrt(Math.Sqrt(1.0 / (1.0 / (C_a * C_a * C_a * C_a) + 1.0 / (C_b * C_b * C_b * C_b))));
		}

		double C_0;
		{
			// for C_0, the shape is independent of hue, so ST are constant. Values picked to roughly be the average values of ST.
			double C_a = L * 0.4;
			double C_b = (1.0 - L) * 0.8;

			// Use a soft minimum function, instead of a sharp triangle shape to get a smooth value for chroma.
			C_0 = Math.Sqrt(1.0 / (1.0 / (C_a * C_a) + 1.0 / (C_b * C_b)));
		}

		return new Cs()
		{
			C_0 = C_0, 
			C_mid = C_mid,
			C_max = C_max
		};
	}

	public static Rgb OkHslToSrgb(double h, double s, double l)
	{
		if (l == 1.0)
		{
			return new Rgb(1.0, 1.0, 1.0);
		}
		else if (l == 0.0)
		{
			return new Rgb(0.0, 0.0, 0.0);
		}

		double a_ = Math.Cos(2.0 * Math.PI * h);
		double b_ = Math.Sin(2.0 * Math.PI * h);
		double L = ToeInverse(l);

		Cs cs = GetCs(L, a_, b_);
		double C_0 = cs.C_0;
		double C_mid = cs.C_mid;
		double C_max = cs.C_max;

		double mid = 0.8;
		double mid_inv = 1.25;

		double C, t, k_0, k_1, k_2;

		if (s < mid)
		{
			t = mid_inv * s;

			k_1 = mid * C_0;
			k_2 = (1.0 - k_1 / C_mid);

			C = t * k_1 / (1.0 - k_2 * t);
		}
		else
		{
			t = (s - mid) / (1 - mid);

			k_0 = C_mid;
			k_1 = (1.0 - mid) * C_mid * C_mid * mid_inv * mid_inv / C_0;
			k_2 = (1.0 - (k_1) / (C_max - C_mid));

			C = k_0 + t * k_1 / (1.0 - k_2 * t);
		}

		Rgb rgb = OklabToLinearSrgb(new Lab(L, C * a_, C * b_));

		return new Rgb(
			SrgbTransferFunction(rgb.R),
			SrgbTransferFunction(rgb.G),
			SrgbTransferFunction(rgb.B)
		);
	}

	public static Hsl SrgbToOkHsl(double r, double g, double b)
	{
		Lab lab = LinearSrgbToOklab(new Rgb(SrgbTransferFunctionInverse(r), SrgbTransferFunctionInverse(g), SrgbTransferFunctionInverse(b)));

		double C = Math.Sqrt(lab.a * lab.a + lab.b * lab.b);
		double a_ = lab.a / C;
		double b_ = lab.b / C;

		double L = lab.L;
		double h = 0.5 + 0.5 * Math.Atan2(-lab.b, -lab.a) / Math.PI;

		Cs cs = GetCs(L, a_, b_);
		double C_0 = cs.C_0;
		double C_mid = cs.C_mid;
		double C_max = cs.C_max;

		// Inverse of the interpolation in OkHslToSrgb:

		double mid = 0.8;
		double mid_inv = 1.25;

		double s;
		if (C < C_mid)
		{
			double k_1 = mid * C_0;
			double k_2 = (1.0 - k_1 / C_mid);

			double t = C / (k_1 + k_2 * C);
			s = t * mid;
		}
		else
		{
			double k_0 = C_mid;
			double k_1 = (1.0 - mid) * C_mid * C_mid * mid_inv * mid_inv / C_0;
			double k_2 = (1.0 - (k_1) / (C_max - C_mid));

			double t = (C - k_0) / (k_1 + k_2 * (C - k_0));
			s = mid + (1.0 - mid) * t;
		}

		return new Hsl(h, s, Toe(L));
	}


	public static Rgb OkHsvToSrgb(double h, double s, double v)
	{
		double a_ = Math.Cos(2.0 * Math.PI * h);
		double b_ = Math.Sin(2.0 * Math.PI * h);

		LC cusp = FindCusp(a_, b_);
		ST ST_max = ToSt(cusp);
		double S_max = ST_max.S;
		double T_max = ST_max.T;
		double S_0 = 0.5;
		double k = 1 - S_0 / S_max;

		// first we compute L and V as if the gamut is a perfect triangle:

		// L, C when v==1:
		double L_v = 1 - s * S_0 / (S_0 + T_max - T_max * k * s);
		double C_v = s * T_max * S_0 / (S_0 + T_max - T_max * k * s);

		double L = v * L_v;
		double C = v * C_v;

		// then we compensate for both Toe and the curved top part of the triangle:
		double L_vt = ToeInverse(L_v);
		double C_vt = C_v * L_vt / L_v;

		double L_new = ToeInverse(L);
		C = C * L_new / L;
		L = L_new;

		Rgb rgb_scale = OklabToLinearSrgb(new Lab(L_vt, a_ * C_vt, b_ * C_vt));
		double scale_L = Math.Cbrt(1.0 / Math.Max(Math.Max(rgb_scale.R, rgb_scale.G), Math.Max(rgb_scale.B, 0.0)));

		L = L * scale_L;
		C = C * scale_L;

		Rgb rgb = OklabToLinearSrgb(new Lab(L, C * a_, C * b_));
			
		return new Rgb(
			SrgbTransferFunction(rgb.R),
			SrgbTransferFunction(rgb.G),
			SrgbTransferFunction(rgb.B)
		);
	}

	public static Hsv SrgbToOkHsv(double r, double g, double b)
	{
		Lab lab = LinearSrgbToOklab(new Rgb(SrgbTransferFunctionInverse(r), SrgbTransferFunctionInverse(g), SrgbTransferFunctionInverse(b)));

		double C = Math.Sqrt(lab.a * lab.a + lab.b * lab.b);
		double a_ = lab.a / C;
		double b_ = lab.b / C;

		double L = lab.L;
		double h = 0.5 + 0.5 * Math.Atan2(-lab.b, -lab.a) / Math.PI;

		LC cusp = FindCusp(a_, b_);
		ST ST_max = ToSt(cusp);
		double S_max = ST_max.S;
		double T_max = ST_max.T;
		double S_0 = 0.5;
		double k = 1 - S_0 / S_max;

		// first we find L_v, C_v, L_vt and C_vt

		double t = T_max / (C + L * T_max);
		double L_v = t * L;
		double C_v = t * C;

		double L_vt = ToeInverse(L_v);
		double C_vt = C_v * L_vt / L_v;

		// we can then use these to invert the step that compensates for the Toe and the curved top part of the triangle:
		Rgb rgb_scale = OklabToLinearSrgb(new Lab(L_vt, a_ * C_vt, b_ * C_vt));
		double scale_L = Math.Cbrt(1.0 / Math.Max(Math.Max(rgb_scale.R, rgb_scale.G), Math.Max(rgb_scale.B, 0.0)));

		L = L / scale_L;
		C = C / scale_L;

		C = C * Toe(L) / L;
		L = Toe(L);

		// we can now compute v and s:

		double v = L / L_v;
		double s = (S_0 + T_max) * C_v / ((T_max * S_0) + T_max * k * C_v);

		return new Hsv(h, s, v);
	}
}