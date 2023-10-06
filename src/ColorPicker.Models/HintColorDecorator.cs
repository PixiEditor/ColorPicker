namespace ColorPicker.Models
{
    public class HintColorDecorator : IColorStateStorage
    {
        private readonly IHintColorStateStorage storage;

        public HintColorDecorator(IHintColorStateStorage storage)
        {
            this.storage = storage;
        }

        public ColorState ColorState
        {
            get => storage.HintColorState;
            set => storage.HintColorState = value;
        }
    }
}