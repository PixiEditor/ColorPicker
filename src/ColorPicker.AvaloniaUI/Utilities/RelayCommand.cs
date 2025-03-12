using System.Windows.Input;

namespace ColorPicker.Utilities;

internal class RelayCommand<T> : ICommand
{
    private Action<T> execute;
    private Func<T, bool> canExecute;

    public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return canExecute == null || canExecute((T)parameter);
    }

    public void Execute(object parameter)
    {
        execute((T)parameter);
    }

    public event EventHandler CanExecuteChanged;
}