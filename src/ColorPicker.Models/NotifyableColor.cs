namespace ColorPicker.Models
{
    public class NotifyableColor : NotifyableObject
    {
        private readonly IColorStateStorage storage;
        private bool isUpdating = false;

        public NotifyableColor(IColorStateStorage colorStateStorage)
        {
            storage = colorStateStorage;
        }

        public double A
        {
            get => storage.ColorState.A * 255;
            set
            {
                if(isUpdating) return;
                var state = storage.ColorState;
                state.A = value / 255;
                storage.ColorState = state;
            }
        }

        public double RGB_R
        {
            get => storage.ColorState.RGB_R * 255;
            set
            {
                if(isUpdating) return;

                var state = storage.ColorState;
                state.RGB_R = value / 255;
                storage.ColorState = state;
            }
        }

        public double RGB_G
        {
            get => storage.ColorState.RGB_G * 255;
            set
            {
                if(isUpdating) return;

                var state = storage.ColorState;
                state.RGB_G = value / 255;
                storage.ColorState = state;
            }
        }

        public double RGB_B
        {
            get => storage.ColorState.RGB_B * 255;
            set
            {
                if(isUpdating) return;

                var state = storage.ColorState;
                state.RGB_B = value / 255;
                storage.ColorState = state;
            }
        }

        public double HSV_H
        {
            get => storage.ColorState.HSV_H;
            set
            {
                if(isUpdating) return;

                var state = storage.ColorState;
                state.HSV_H = value;
                storage.ColorState = state;
            }
        }

        public double HSV_S
        {
            get => storage.ColorState.HSV_S * 100;
            set
            {
                if(isUpdating) return;

                var state = storage.ColorState;
                state.HSV_S = value / 100;
                storage.ColorState = state;
            }
        }

        public double HSV_V
        {
            get => storage.ColorState.HSV_V * 100;
            set
            {
                if(isUpdating) return;

                var state = storage.ColorState;
                state.HSV_V = value / 100;
                storage.ColorState = state;
            }
        }

        public double HSL_H
        {
            get => storage.ColorState.HSL_H;
            set
            {
                if(isUpdating) return;

                var state = storage.ColorState;
                state.HSL_H = value;
                storage.ColorState = state;
            }
        }

        public double HSL_S
        {
            get => storage.ColorState.HSL_S * 100;
            set
            {
                if(isUpdating) return;

                var state = storage.ColorState;
                state.HSL_S = value / 100;
                storage.ColorState = state;
            }
        }

        public double HSL_L
        {
            get => storage.ColorState.HSL_L * 100;
            set
            {
                if(isUpdating) return;

                var state = storage.ColorState;
                state.HSL_L = value / 100;
                storage.ColorState = state;
            }
        }

        public double OKHSV_H
        {
            get => storage.ColorState.OKHSV_H;
            set
            {
                if(isUpdating) return;

                var state = storage.ColorState;
                state.OKHSV_H = value;
                storage.ColorState = state;
            }
        }

        public double OKHSV_S
        {
            get => storage.ColorState.OKHSV_S * 100;
            set
            {
                if(isUpdating) return;

                var state = storage.ColorState;
                state.OKHSV_S = value / 100;
                storage.ColorState = state;
            }
        }

        public double OKHSV_V
        {
            get => storage.ColorState.OKHSV_V * 100;
            set
            {
                if(isUpdating) return;

                var state = storage.ColorState;
                state.OKHSV_V = value / 100;
                storage.ColorState = state;
            }
        }

        public double OKHSL_H
        {
            get => storage.ColorState.OKHSL_H;
            set
            {
                if(isUpdating) return;

                var state = storage.ColorState;
                state.OKHSL_H = value;
                storage.ColorState = state;
            }
        }

        public double OKHSL_S
        {
            get => storage.ColorState.OKHSL_S * 100;
            set
            {
                if(isUpdating) return;

                var state = storage.ColorState;
                state.OKHSL_S = value / 100;
                storage.ColorState = state;
            }
        }

        public double OKHSL_L
        {
            get => storage.ColorState.OKHSL_L * 100;
            set
            {
                if(isUpdating) return;

                var state = storage.ColorState;
                state.OKHSL_L = value / 100;
                storage.ColorState = state;
            }
        }
        
        public void UpdateEverything(ColorState oldValue)
        {
            var currentValue = storage.ColorState;
            if(isUpdating) return;
            isUpdating = true;
            if (currentValue.A != oldValue.A) RaisePropertyChanged(nameof(A));

            if (currentValue.RGB_R != oldValue.RGB_R) RaisePropertyChanged(nameof(RGB_R));
            if (currentValue.RGB_G != oldValue.RGB_G) RaisePropertyChanged(nameof(RGB_G));
            if (currentValue.RGB_B != oldValue.RGB_B) RaisePropertyChanged(nameof(RGB_B));

            if (currentValue.HSV_H != oldValue.HSV_H) RaisePropertyChanged(nameof(HSV_H));
            if (currentValue.HSV_S != oldValue.HSV_S) RaisePropertyChanged(nameof(HSV_S));
            if (currentValue.HSV_V != oldValue.HSV_V) RaisePropertyChanged(nameof(HSV_V));

            if (currentValue.HSL_H != oldValue.HSL_H) RaisePropertyChanged(nameof(HSL_H));
            if (currentValue.HSL_S != oldValue.HSL_S) RaisePropertyChanged(nameof(HSL_S));
            if (currentValue.HSL_L != oldValue.HSL_L) RaisePropertyChanged(nameof(HSL_L));
            
            if (currentValue.OKHSV_H != oldValue.OKHSV_H) RaisePropertyChanged(nameof(OKHSV_H));
            if (currentValue.OKHSV_S != oldValue.OKHSV_S) RaisePropertyChanged(nameof(OKHSV_S));
            if (currentValue.OKHSV_V != oldValue.OKHSV_V) RaisePropertyChanged(nameof(OKHSV_V));

            if (currentValue.OKHSL_H != oldValue.OKHSL_H) RaisePropertyChanged(nameof(OKHSL_H));
            if (currentValue.OKHSL_S != oldValue.OKHSL_S) RaisePropertyChanged(nameof(OKHSL_S));
            if (currentValue.OKHSL_L != oldValue.OKHSL_L) RaisePropertyChanged(nameof(OKHSL_L));
            isUpdating = false;
        }
    }
}