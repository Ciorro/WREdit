using System.Windows.Input;

namespace WREdit
{
    class RelayCommand : ICommand
    {
        private Action<object?> _execute;
        private Func<object?, bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = (_) => execute();

            if (canExecute is not null)
            {
                _canExecute = (_) => canExecute();
            }
        }

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object? parameter)
        {
            if (CanExecute(parameter))
            {
                _execute?.Invoke(parameter);
            }
        }
    }
}
