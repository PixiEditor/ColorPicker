using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPicker.Models
{
    public class NotifyableColor : NotifyableObject
    {
        IColorStateStorage storage;
        public NotifyableColor(IColorStateStorage colorStateStorage)
        {
            storage = colorStateStorage;
        }

        public double A
        {
            get => storage.ColorState.A * 255;
            set
            {
                var state = storage.ColorState;
                state.A = value / 255;
                storage.ColorState = state;
                RaisePropertyChanged(nameof(A));
            }
        }

        public double RGB_R
        {
            get => storage.ColorState.RGB_R * 255;
            set
            {
                var state = storage.ColorState;
                state.RGB_R = value / 255;
                storage.ColorState = state;
                RaisePropertyChanged(nameof(RGB_R));

                RaisePropertyChanged(nameof(HSV_H));
                RaisePropertyChanged(nameof(HSV_S));
                RaisePropertyChanged(nameof(HSV_V));
            }
        }

        public double RGB_G
        {
            get => storage.ColorState.RGB_G * 255;
            set
            {
                var state = storage.ColorState;
                state.RGB_G = value / 255;
                storage.ColorState = state;
                RaisePropertyChanged(nameof(RGB_G));

                RaisePropertyChanged(nameof(HSV_H));
                RaisePropertyChanged(nameof(HSV_S));
                RaisePropertyChanged(nameof(HSV_V));
            }
        }

        public double RGB_B
        {
            get => storage.ColorState.RGB_B * 255;
            set
            {
                var state = storage.ColorState;
                state.RGB_B = value / 255;
                storage.ColorState = state;
                RaisePropertyChanged(nameof(RGB_B));

                RaisePropertyChanged(nameof(HSV_H));
                RaisePropertyChanged(nameof(HSV_S));
                RaisePropertyChanged(nameof(HSV_V));
            }
        }

        public double HSV_H
        {
            get => storage.ColorState.HSV_H;
            set
            {
                var state = storage.ColorState;
                state.HSV_H = value;
                storage.ColorState = state;
                RaisePropertyChanged(nameof(HSV_H));

                RaisePropertyChanged(nameof(RGB_R));
                RaisePropertyChanged(nameof(RGB_G));
                RaisePropertyChanged(nameof(RGB_B));
            }
        }

        public double HSV_S
        {
            get => storage.ColorState.HSV_S * 100;
            set
            {
                var state = storage.ColorState;
                state.HSV_S = value / 100;
                storage.ColorState = state;
                RaisePropertyChanged(nameof(HSV_S));

                RaisePropertyChanged(nameof(RGB_R));
                RaisePropertyChanged(nameof(RGB_G));
                RaisePropertyChanged(nameof(RGB_B));
            }
        }

        public double HSV_V
        {
            get => storage.ColorState.HSV_V * 100;
            set
            {
                var state = storage.ColorState;
                state.HSV_V = value / 100;
                storage.ColorState = state;
                RaisePropertyChanged(nameof(HSV_V));

                RaisePropertyChanged(nameof(RGB_R));
                RaisePropertyChanged(nameof(RGB_G));
                RaisePropertyChanged(nameof(RGB_B));
            }
        }
    }
}
