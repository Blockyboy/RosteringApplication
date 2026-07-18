using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RosteringApplication.Model;

namespace RosteringApplication.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<Employee> Employees { get; set; }

        public MainViewModel()
        {
            Employees = [ new Employee(1, "Ivan", "Pribster", "Worker") ];
        }
    }
}
