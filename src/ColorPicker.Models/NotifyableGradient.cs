namespace ColorPicker.Models
{
    public class NotifyableGradient : NotifyableObject
    {
        private readonly IGradientStorage storage;
        private bool isUpdating = false;

        public NotifyableGradient(IGradientStorage gradientStorage)
        {
            storage = gradientStorage;
        }
    }
}
