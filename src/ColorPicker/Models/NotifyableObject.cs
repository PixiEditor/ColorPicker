using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ColorPicker.Models
{
    public class NotifyableObject : INotifyPropertyChanged
    {
        [field: NonSerialized] public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged(string property)
        {
            if (property != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
