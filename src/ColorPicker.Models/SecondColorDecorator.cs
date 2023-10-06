namespace ColorPicker.Models
{
    public class SecondColorDecorator : IColorStateStorage
    {
        private readonly ISecondColorStorage storage;

        public SecondColorDecorator(ISecondColorStorage storage)
        {
            this.storage = storage;
        }

        public ColorState ColorState
        {
            get => storage.SecondColorState;
            set => storage.SecondColorState = value;
        }
    }
}