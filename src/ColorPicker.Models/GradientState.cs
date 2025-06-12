using System;
using System.Collections.Generic;

namespace ColorPicker.Models
{
    public struct GradientState
    {
        private List<GradientStop> stops;
        public IReadOnlyList<GradientStop> Stops => stops;

        public double LinearStartPointX { get; set; }
        public double LinearStartPointY { get; set; }
        public double LinearEndPointX { get; set; }
        public double LinearEndPointY { get; set; }

        public double RadialCenterX { get; set; }
        public double RadialCenterY { get; set; }
        public double RadialRadius { get; set; }

        public double ConicAngle { get; set; }
        public double ConicCenterX { get; set; }
        public double ConicCenterY { get; set; }
        public bool AbsoluteUnits { get; set; }
        public Matrix Transform { get; set; } = Matrix.Identity;

        public GradientState(List<GradientStop> stops)
        {
            this.stops = stops;
            LinearStartPointX = 0;
            LinearStartPointY = 0;
            LinearEndPointX = 1;
            LinearEndPointY = 0;
            RadialCenterX = 0.5;
            RadialCenterY = 0.5;
            RadialRadius = 0.5;
            ConicAngle = 0;
            ConicCenterX = 0.5;
            ConicCenterY = 0.5;
            AbsoluteUnits = false;
            Transform = Matrix.Identity;
        }

        public GradientState WithUpdatedStop(int stopIndex, GradientStop newStop)
        {
            GradientState newStopState = new GradientState(new List<GradientStop>(stops))
            {
                stops = { [stopIndex] = newStop },
                LinearStartPointX = LinearStartPointX,
                LinearStartPointY = LinearStartPointY,
                LinearEndPointX = LinearEndPointX,
                LinearEndPointY = LinearEndPointY,
                RadialCenterX = RadialCenterX,
                RadialCenterY = RadialCenterY,
                RadialRadius = RadialRadius,
                ConicAngle = ConicAngle,
                ConicCenterX = ConicCenterX,
                ConicCenterY = ConicCenterY,
                AbsoluteUnits = AbsoluteUnits,
                Transform = Transform
            };

            return newStopState;
        }

        public ColorState Evaluate(double offset)
        {
            GradientStop stop0 = stops[0];
            GradientStop stop1 = stops[stops.Count - 1];

            if (offset <= stop0.Offset)
            {
                return stop0.ColorState;
            }

            if (offset >= stop1.Offset)
            {
                return stop1.ColorState;
            }

            for (int i = 0; i < stops.Count - 1; i++)
            {
                GradientStop current = stops[i];
                GradientStop next = stops[i + 1];

                if (offset >= current.Offset && offset <= next.Offset)
                {
                    double t = (offset - current.Offset) / (next.Offset - current.Offset);
                    return ColorState.Lerp(current.ColorState, next.ColorState, t);
                }
            }

            return stops[0].ColorState;
        }

        public GradientState WithAddedStop(GradientStop gradientStop)
        {
            List<GradientStop> newStops = new List<GradientStop>(stops) { gradientStop };
            newStops.Sort((a, b) => a.Offset.CompareTo(b.Offset));
            return new GradientState(newStops)
            {
                LinearStartPointX = LinearStartPointX,
                LinearStartPointY = LinearStartPointY,
                LinearEndPointX = LinearEndPointX,
                LinearEndPointY = LinearEndPointY,
                RadialCenterX = RadialCenterX,
                RadialCenterY = RadialCenterY,
                RadialRadius = RadialRadius,
                ConicAngle = ConicAngle,
                ConicCenterX = ConicCenterX,
                ConicCenterY = ConicCenterY,
                AbsoluteUnits = AbsoluteUnits,
                Transform = Transform
            };
        }

        public GradientState WitRemovedStop(int index)
        {
            List<GradientStop> newStops = new List<GradientStop>(stops);
            newStops.RemoveAt(index);
            return new GradientState(newStops)
            {
                LinearStartPointX = LinearStartPointX,
                LinearStartPointY = LinearStartPointY,
                LinearEndPointX = LinearEndPointX,
                LinearEndPointY = LinearEndPointY,
                RadialCenterX = RadialCenterX,
                RadialCenterY = RadialCenterY,
                RadialRadius = RadialRadius,
                ConicAngle = ConicAngle,
                ConicCenterX = ConicCenterX,
                ConicCenterY = ConicCenterY,
                AbsoluteUnits = AbsoluteUnits,
                Transform = Transform
            };
        }
    }

    public struct GradientStop
    {
        public ColorState ColorState { get; set; }
        public double Offset { get; set; }
    }

    public struct Matrix
    {
        public static readonly Matrix Identity = new() { ScaleX = 1f, ScaleY = 1f };

        /// <summary>Gets or sets the scaling in the x-direction.</summary>
        /// <value />
        public double ScaleX { get; set; }

        /// <summary>Gets or sets the skew in the x-direction.</summary>
        /// <value />
        public double SkewX { get; set; }

        /// <summary>Get or sets the translation in the x-direction.</summary>
        /// <value />
        public double TransX { get; set; }

        /// <summary>Gets or sets the skew in the y-direction.</summary>
        /// <value />
        public double SkewY { get; set; }

        /// <summary>Gets or sets the scaling in the y-direction.</summary>
        /// <value />
        public double ScaleY { get; set; }

        /// <summary>Get or sets the translation in the y-direction.</summary>
        /// <value />
        public double TransY { get; set; }


        public Matrix(double scaleX, double skewX, double transX,
            double skewY, double scaleY, double transY)
        {
            ScaleX = scaleX;
            SkewX = skewX;
            TransX = transX;
            SkewY = skewY;
            ScaleY = scaleY;
            TransY = transY;
        }
    }
}
