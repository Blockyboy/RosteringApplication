using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RosteringApplication.Model;
using System.Windows.Input;
using RosteringApplication.Command;
using RosteringApplication.View;
using System.Net.NetworkInformation;

namespace RosteringApplication.ViewModel
{
    public class MainViewModel
    {
        public static ObservableCollection<Employee> Employees { get; } = new  ObservableCollection<Employee>(); 

        public static int CurrentID = 0;

        public ICommand OpenEmployeeAddWindow {  get; set; }
        public ICommand OpenEmployeeShiftWindow { get; set; }

        public MainViewModel()
        {
            OpenEmployeeAddWindow = new CommunicateCommand(ExecuteEmployeeAddWindow, CanOpenEmployeeAddWindow);
            OpenEmployeeShiftWindow = new CommunicateCommand(ExecuteEmployeeShiftWindow, CanOpenEmployeeShiftWindow);
        }

        public static void AddEmployee(Employee employee)
        {
            employee.Id = CurrentID;
            ++CurrentID;

            Employees.Add(employee);
        }

        private bool CanOpenEmployeeAddWindow(object obj)
        {
            return true;
        }

        private void ExecuteEmployeeAddWindow(object obj)
        {
            AddEmployee addEmployee = new();
            addEmployee.Show();
        }

        private bool CanOpenEmployeeShiftWindow(object obj)
        {
            return true;
        }

        private void ExecuteEmployeeShiftWindow(object obj)
        {
            if (obj != null)
            {
                EmployeeShifts employeeShifts = new();
                EmployeeShiftsViewModel addEmployeeViewModel = new EmployeeShiftsViewModel((Employee)obj);
                employeeShifts.DataContext = addEmployeeViewModel;
                employeeShifts.Show();
            }
        }
    }
}
