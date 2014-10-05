using System;
using System.Windows.Input;

namespace Countdown
{
    public class Command : ICommand
    {
        private bool _canExecute;
        private readonly Action _action;

        public bool CanExecute
        {
            get { return _canExecute; }
            set
            {
                if (_canExecute.Equals(value))
                    return;
                _canExecute = value;
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, new EventArgs());
            }
        }
        
        public Command(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return this.CanExecute;
        }

        public event EventHandler CanExecuteChanged;

        void ICommand.Execute(object parameter)
        {
            _action();
        }
    }
}
