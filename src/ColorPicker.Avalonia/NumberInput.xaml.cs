using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using System;
using System.Text.RegularExpressions;

namespace ColorPicker
{
    /// <summary>
    ///     Interaction logic for NumerInput.xaml
    /// </summary>
    public partial class NumberInput : UserControl
    {
        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly StyledProperty<float> ValueProperty =
            AvaloniaProperty.Register<PortableColorPicker, float>("Value", 0f);

        // Using a DependencyProperty as the backing store for Min.  This enables animation, styling, binding, etc...
        public static readonly StyledProperty<float> MinProperty =
            AvaloniaProperty.Register<PortableColorPicker, float>("Min", float.NegativeInfinity);

        // Using a DependencyProperty as the backing store for Max.  This enables animation, styling, binding, etc...
        public static readonly StyledProperty<float> MaxProperty =
            AvaloniaProperty.Register<PortableColorPicker, float>("Max", float.PositiveInfinity);


        public NumberInput()
        {
             AvaloniaXamlLoader.Load(this);
            this.WhenAnyValue(x => x.Value).Subscribe(x => Math.Clamp((float)x, Min, Max));
        }

        private void NumberInput_TextInput(object sender, Avalonia.Input.TextInputEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        public float Value
        {
            get => (float) GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public float Min
        {
            get => (float) GetValue(MinProperty);
            set => SetValue(MinProperty, value);
        }


        public float Max
        {
            get => (float) GetValue(MaxProperty);
            set => SetValue(MaxProperty, value);
        }
    }
}