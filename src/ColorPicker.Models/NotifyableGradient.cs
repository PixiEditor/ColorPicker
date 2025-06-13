namespace ColorPicker.Models
{
    public class NotifyableGradient : NotifyableObject
    {
        private readonly IGradientStorage gradientStorage;

        private bool isUpdating = false;

        public double LinearStartPointX
        {
            get => gradientStorage.GradientState.LinearStartPointX;
            set
            {
                if (isUpdating) return;
                var state = gradientStorage.GradientState;
                state.LinearStartPointX = value;
                gradientStorage.GradientState = state;
            }
        }

        public double LinearStartPointY
        {
            get => gradientStorage.GradientState.LinearStartPointY;
            set
            {
                if (isUpdating) return;
                var state = gradientStorage.GradientState;
                state.LinearStartPointY = value;
                gradientStorage.GradientState = state;
            }
        }

        public double LinearEndPointX
        {
            get => gradientStorage.GradientState.LinearEndPointX;
            set
            {
                if (isUpdating) return;
                var state = gradientStorage.GradientState;
                state.LinearEndPointX = value;
                gradientStorage.GradientState = state;
            }
        }

        public double LinearEndPointY
        {
            get => gradientStorage.GradientState.LinearEndPointY;
            set
            {
                if (isUpdating) return;
                var state = gradientStorage.GradientState;
                state.LinearEndPointY = value;
                gradientStorage.GradientState = state;
            }
        }

        public double RadialCenterX
        {
            get => gradientStorage.GradientState.RadialCenterX;
            set
            {
                if (isUpdating) return;
                var state = gradientStorage.GradientState;
                state.RadialCenterX = value;
                gradientStorage.GradientState = state;
            }
        }

        public double RadialCenterY
        {
            get => gradientStorage.GradientState.RadialCenterY;
            set
            {
                if (isUpdating) return;
                var state = gradientStorage.GradientState;
                state.RadialCenterY = value;
                gradientStorage.GradientState = state;
            }
        }

        public double RadialRadius
        {
            get => gradientStorage.GradientState.RadialRadius;
            set
            {
                if (isUpdating) return;
                var state = gradientStorage.GradientState;
                state.RadialRadius = value;
                gradientStorage.GradientState = state;
            }
        }

        public double ConicCenterX
        {
            get => gradientStorage.GradientState.ConicCenterX;
            set
            {
                if (isUpdating) return;
                var state = gradientStorage.GradientState;
                state.ConicCenterX = value;
                gradientStorage.GradientState = state;
            }
        }

        public double ConicCenterY
        {
            get => gradientStorage.GradientState.ConicCenterY;
            set
            {
                if (isUpdating) return;
                var state = gradientStorage.GradientState;
                state.ConicCenterY = value;
                gradientStorage.GradientState = state;
            }
        }

        public double ConicAngle
        {
            get => gradientStorage.GradientState.ConicAngle;
            set
            {
                if (isUpdating) return;
                var state = gradientStorage.GradientState;
                state.ConicAngle = value;
                gradientStorage.GradientState = state;
            }
        }

        public NotifyableGradient(IGradientStorage gradientStorage)
        {
            this.gradientStorage = gradientStorage;
        }

        public void UpdateEverything(GradientState old)
        {
            if (isUpdating) return;

            isUpdating = true;

            if (LinearStartPointX != old.LinearStartPointX)
                RaisePropertyChanged(nameof(LinearStartPointX));
            if (LinearStartPointY != old.LinearStartPointY)
                RaisePropertyChanged(nameof(LinearStartPointY));
            if (LinearEndPointX != old.LinearEndPointX)
                RaisePropertyChanged(nameof(LinearEndPointX));
            if (LinearEndPointY != old.LinearEndPointY)
                RaisePropertyChanged(nameof(LinearEndPointY));
            if (RadialCenterX != old.RadialCenterX)
                RaisePropertyChanged(nameof(RadialCenterX));
            if (RadialCenterY != old.RadialCenterY)
                RaisePropertyChanged(nameof(RadialCenterY));
            if (RadialRadius != old.RadialRadius)
                RaisePropertyChanged(nameof(RadialRadius));
            if (ConicCenterX != old.ConicCenterX)
                RaisePropertyChanged(nameof(ConicCenterX));
            if (ConicCenterY != old.ConicCenterY)
                RaisePropertyChanged(nameof(ConicCenterY));
            if (ConicAngle != old.ConicAngle)
                RaisePropertyChanged(nameof(ConicAngle));

            isUpdating = false;
        }
    }
}
