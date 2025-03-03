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