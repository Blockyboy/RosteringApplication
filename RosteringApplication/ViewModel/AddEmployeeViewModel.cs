using RosteringApplication.Command;
using RosteringApplication.Model;
using RosteringApplication.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RosteringApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;

namespace RosteringApplication.ViewModel
{
    public class AddEmployeeViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public ICommand AddEmployee { get; set; }

        public ICommand Cancel { get; set; }

        public AddEmployeeViewModel()
        {
            AddEmployee = new CommunicateCommand(ExecuteEmployeeAdd, CanAddEmployee);
            Cancel = new CommunicateCommand(ExecuteCancel, CanCancel);
        }

        private bool CanAddEmployee(object obj)
        {
            return true;
        }

        private void ExecuteEmployeeAdd(object obj)
        {
            Employee addedEmployee = new Employee(FirstName, LastName, Role);
            MainViewModel.AddEmployee(addedEmployee);
            var jsonData = JsonConvert.SerializeObject(addedEmployee);
            MainViewModel.WriteEmployeeFile(jsonData);
        }

        private bool CanCancel(object obj)
        {
            return true;
        }

        private void ExecuteCancel(object obj)
        {
            ((Window)obj).Close();
        }
    }
}
