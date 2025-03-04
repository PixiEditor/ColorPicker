using System;
using System.Collections.Generic;

namespace ColorPicker.Models
{
    public struct GradientState
    {
        private List<GradientStop> stops;
        public IReadOnlyList<GradientStop> Stops
        {
            get => stops;
        }

        public GradientState(List<GradientStop> stops)
        {
            this.stops = stops;
        }

        public GradientState WithUpdatedStop(int stopIndex, GradientStop newStop)
        {
            GradientState newStopState = new GradientState(stops);
            newStopState.stops[stopIndex] = newStop;
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
            List<GradientStop> newStops = new List<GradientStop>(stops);
            newStops.Add(gradientStop);
            newStops.Sort((a, b) => a.Offset.CompareTo(b.Offset));
            return new GradientState(newStops);
        }

        public GradientState WitRemovedStop(int index)
        {
            List<GradientStop> newStops = new List<GradientStop>(stops);
            newStops.RemoveAt(index);
            return new GradientState(newStops);
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