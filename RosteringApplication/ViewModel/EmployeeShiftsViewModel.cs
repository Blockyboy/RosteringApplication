using RosteringApplication.Command;
using RosteringApplication.Model;
using RosteringApplication.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;

namespace RosteringApplication.ViewModel
{
    public class EmployeeShiftsViewModel
    {
        public Employee Employee { get; set; }

        public ICommand OpenShiftAddWindow { get; set; }

        public EmployeeShiftsViewModel(Employee employee)
        {
            OpenShiftAddWindow = new CommunicateCommand(ExecuteShiftAddWindow, CanOpenShiftAddWindow);
            Employee = employee;
        }

        public void AddShift(Shift shift)
        {
            Employee.AddShift(shift);
        }

        private bool CanOpenShiftAddWindow(object obj)
        {
            return true;
        }

        private void ExecuteShiftAddWindow(object obj)
        {
            AddShift addShift = new();
            AddShiftViewModel addShiftViewModel = new AddShiftViewModel(this);
            addShift.DataContext = addShiftViewModel;
            addShift.Show();
        }
    }
}
