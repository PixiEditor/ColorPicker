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
        public double RadialOriginX { get; set; }
        public double RadialOriginY { get; set; }
        public double RadialRadiusX { get; set; }
        public double RadialRadiusY { get; set; }

        public double ConicAngle { get; set; }
        public double ConicCenterX { get; set; }
        public double ConicCenterY { get; set; }

        public GradientState(List<GradientStop> stops)
        {
            this.stops = stops;
            LinearStartPointX = 0;
            LinearStartPointY = 0;
            LinearEndPointX = 1;
            LinearEndPointY = 0;
            RadialCenterX = 0.5;
            RadialCenterY = 0.5;
            RadialOriginX = 0.5;
            RadialOriginY = 0.5;
            RadialRadiusX = 0.5;
            RadialRadiusY = 0.5;
            ConicAngle = 0;
            ConicCenterX = 0.5;
            ConicCenterY = 0.5;
        }

        public GradientState WithUpdatedStop(int stopIndex, GradientStop newStop)
        {
            GradientState newStopState = new GradientState(stops)
            {
                stops =
                {
                    [stopIndex] = newStop
                },
                LinearStartPointX = LinearStartPointX,
                LinearStartPointY = LinearStartPointY,
                LinearEndPointX = LinearEndPointX,
                LinearEndPointY = LinearEndPointY,
                RadialCenterX = RadialCenterX,
                RadialCenterY = RadialCenterY,
                RadialOriginX = RadialOriginX,
                RadialOriginY = RadialOriginY,
                RadialRadiusX = RadialRadiusX,
                RadialRadiusY = RadialRadiusY,
                ConicAngle = ConicAngle,
                ConicCenterX = ConicCenterX,
                ConicCenterY = ConicCenterY
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
                RadialOriginX = RadialOriginX,
                RadialOriginY = RadialOriginY,
                RadialRadiusX = RadialRadiusX,
                RadialRadiusY = RadialRadiusY,
                ConicAngle = ConicAngle,
                ConicCenterX = ConicCenterX,
                ConicCenterY = ConicCenterY
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
                RadialOriginX = RadialOriginX,
                RadialOriginY = RadialOriginY,
                RadialRadiusX = RadialRadiusX,
                RadialRadiusY = RadialRadiusY,
                ConicAngle = ConicAngle,
                ConicCenterX = ConicCenterX,
                ConicCenterY = ConicCenterY
            };
        }
    }

    public struct GradientStop : IEquatable<GradientStop>
    {
        public ColorState ColorState { get; set; }
        public double Offset { get; set; }

        public bool Equals(GradientStop other)
        {
            return ColorState.Equals(other.ColorState) && Offset.Equals(other.Offset);
        }

        public override bool Equals(object obj)
        {
            return obj is GradientStop other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (ColorState.GetHashCode() * 397) ^ Offset.GetHashCode();
            }
        }
    }
}