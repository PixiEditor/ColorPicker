using Avalonia.Reactive;

namespace ColorPicker.UIExtensions;

public static class ObservableSubscribeExtensions
{
    public static void Subscribe<T>(this IObservable<T> observable, Action<T> onNext)
    {
        _ = observable.Subscribe(new AnonymousObserver<T>(onNext));
    }
}
