using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Reactive;

namespace ColorPicker.Converters
{
    internal class ColorToHexConverter : AvaloniaObject, IValueConverter
    {
        public static readonly StyledProperty<bool> ShowAlphaProperty = AvaloniaProperty.Register<ColorToHexConverter, bool>(
            nameof(ShowAlpha), true);

        public bool ShowAlpha
        {
            get => GetValue(ShowAlphaProperty);
            set => SetValue(ShowAlphaProperty, value);
        }

        public event EventHandler OnShowAlphaChange;

        static ColorToHexConverter()
        {
            ShowAlphaProperty.Changed.Subscribe(
                new AnonymousObserver<AvaloniaPropertyChangedEventArgs<bool>>(ShowAlphaChangedCallback));
        }

        public void RaiseShowAlphaChange()
        {
            OnShowAlphaChange(this, EventArgs.Empty);
        }

        private static void ShowAlphaChangedCallback(AvaloniaPropertyChangedEventArgs<bool> d)
        {
            ((ColorToHexConverter)d.Sender).RaiseShowAlphaChange();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!ShowAlpha)
                return ConvertNoAlpha(value);
            return ((Color)value).ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!ShowAlpha)
                return ConvertBackNoAlpha(value);
            string text = (string)value;
            text = Regex.Replace(text.ToUpperInvariant(), @"[^0-9A-F]", "");
            StringBuilder final = new StringBuilder();
            if (text.Length == 3) //short hex with no alpha
            {
                final.Append("#FF").Append(text[0]).Append(text[0]).Append(text[1]).Append(text[1]).Append(text[2]).Append(text[2]);
            }
            else if (text.Length == 4) //short hex with alpha
            {
                final.Append("#").Append(text[0]).Append(text[0]).Append(text[1]).Append(text[1]).Append(text[2]).Append(text[2]).Append(text[3]).Append(text[3]);
            }
            else if (text.Length == 6) //hex with no alpha
            {
                final.Append("#FF").Append(text);
            }
            else
            {
                final.Append("#").Append(text);
            }
            try
            {
                return Color.Parse(final.ToString());
            }
            catch (Exception)
            {
                return AvaloniaProperty.UnsetValue;
            }
        }

        public object ConvertNoAlpha(object value)
        {
            return "#" + ((Color)value).ToString().Substring(3, 6);
        }

        public object ConvertBackNoAlpha(object value)
        {
            string text = (string)value;
            text = Regex.Replace(text.ToUpperInvariant(), @"[^0-9A-F]", "");
            StringBuilder final = new StringBuilder();
            if (text.Length == 3) //short hex
            {
                final.Append("#FF").Append(text[0]).Append(text[0]).Append(text[1]).Append(text[1]).Append(text[2]).Append(text[2]);
            }
            else if (text.Length == 4)
            {
                return AvaloniaProperty.UnsetValue;
            }
            else if (text.Length > 6)
            {
                return AvaloniaProperty.UnsetValue;
            }
            else //regular hex
            {
                final.Append("#").Append(text);
            }
            try
            {
                return Color.Parse(final.ToString());
            }
            catch (Exception)
            {
                return AvaloniaProperty.UnsetValue;
            }
        }
    }
}
