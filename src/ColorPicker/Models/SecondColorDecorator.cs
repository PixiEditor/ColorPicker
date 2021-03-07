namespace ColorPicker.Models
{
    class SecondColorDecorator : IColorStateStorage
    {
        public ColorState ColorState
        {
            get => display.SecondColorState;
            set => display.SecondColorState = value;
        }
        private ColorDisplay display;
        public SecondColorDecorator(ColorDisplay display)
        {
            this.display = display;
        }
    }
}
