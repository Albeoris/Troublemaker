using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Troublemaker.Editor.Framework
{
    public abstract class BaseCommand : ICommand
    {
        public virtual Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public abstract void Execute(Object parameter);

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}
