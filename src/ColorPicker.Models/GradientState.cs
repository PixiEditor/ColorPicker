using System.Collections.Generic;

namespace ColorPicker.Models
{
    public class GradientState
    {
        public List<GradientStop> GradientStops { get; set; }

        public GradientState(List<GradientStop> gradientStops)
        {
            GradientStops = gradientStops;
        }
    }

    public struct GradientStop
    {
        public ColorState ColorState { get; set; }
        public double Offset { get; set; }
    }
}