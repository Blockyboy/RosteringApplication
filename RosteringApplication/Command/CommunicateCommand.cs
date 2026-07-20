using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RosteringApplication.Command
{
    public class CommunicateCommand : ICommand
    {

        public Action<object> _Execute {  get; set; }

        public Predicate<object> _CanExecute { get; set; }

        public CommunicateCommand(Action<object> methodToExecute, Predicate<object> canExecute)
        {
            _Execute = methodToExecute;
            _CanExecute = canExecute;
        }

        public void Execute(object? parameter)
        {
            _Execute(parameter);
        }

        public bool CanExecute(object? parameter)
        {
            return _CanExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
