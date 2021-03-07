using ColorPicker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorPicker
{
    public partial class ColorSliders : UserControl, IColorStateStorage
    {
        public static DependencyProperty ColorStateProperty = 
            DependencyProperty.Register(nameof(ColorState), typeof(ColorState), typeof(ColorSliders), 
                new PropertyMetadata(new ColorState(0,0,0,1,0,0,0), OnColorStatePropertyChange));
        
        public ColorState ColorState
        {
            get => (ColorState)GetValue(ColorStateProperty);
            set => SetValue(ColorStateProperty, value);
        }

        public NotifyableColor Color { 
            get; 
            set; 
        }

        public ColorSliders()
        {
            Color = new NotifyableColor(this);
            InitializeComponent();
        }

        private static void OnColorStatePropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((ColorSliders)d).Color.UpdateEverything((ColorState)args.OldValue);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
