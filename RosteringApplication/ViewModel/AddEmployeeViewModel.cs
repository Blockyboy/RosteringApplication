using RosteringApplication.Command;
using RosteringApplication.Model;
using RosteringApplication.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RosteringApplication.ViewModel
{
    public class AddEmployeeViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public ICommand AddEmployee { get; set; }

        public AddEmployeeViewModel()
        {
            AddEmployee = new CommunicateCommand(ExecuteEmployeeAdd, CanAddEmployee);
        }

        private bool CanAddEmployee(object obj)
        {
            return true;
        }

        private void ExecuteEmployeeAdd(object obj)
        {
            Employee addedEmployee = new Employee(FirstName, LastName, Role);
            MainViewModel.AddEmployee(addedEmployee);
        }
    }
}
