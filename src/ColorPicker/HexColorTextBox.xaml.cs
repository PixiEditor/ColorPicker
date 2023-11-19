using System;
using System.Windows;
using System.Windows.Controls;
using ColorPicker.Models;

namespace ColorPicker
{
    public partial class HexColorTextBox : PickerControlBase
    {
        public static readonly DependencyProperty ShowAlphaProperty =
            DependencyProperty.Register(nameof(ShowAlpha), typeof(bool), typeof(HexColorTextBox),
                new PropertyMetadata(true));

        public static readonly DependencyProperty HexRepresentationProperty = 
            DependencyProperty.Register(nameof(HexRepresentation), typeof(HexRepresentationType), typeof(HexColorTextBox),
                new PropertyMetadata(HexRepresentationType.RGBA));

        public HexRepresentationType HexRepresentation
        {
            get => (HexRepresentationType)GetValue(HexRepresentationProperty);
            set => SetValue(HexRepresentationProperty, value);
        }

        
        public HexColorTextBox()
        {
            InitializeComponent();
        }

        public bool ShowAlpha
        {
            get => (bool)GetValue(ShowAlphaProperty);
            set => SetValue(ShowAlphaProperty, value);
        }

        private void RefreshTextbox(object sender, EventArgs e)
        {
            textbox.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
        }
    }
}