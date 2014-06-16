using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_Uebung
{
    public class RelayCommand : ICommand
    {
        private Action action;
        private Func<bool> canExecute;
        public RelayCommand(Action action)
        {
            this.action = action;
        }

        public RelayCommand(Action action, Func<bool> canExecute)
            : this(action)
        {
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
                return canExecute();
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (action != null)
                action();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private Action<T> action;
        public RelayCommand(Action<T> action)
        {
            this.action = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (action != null)
                action((T)parameter);
        }
    }
}
