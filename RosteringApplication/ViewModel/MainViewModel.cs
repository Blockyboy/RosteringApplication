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
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Text.Json;
using RosteringApplication;
using System.Runtime.InteropServices;
using Microsoft.Xaml.Behaviors.Core;

namespace RosteringApplication.ViewModel
{
    public class MainViewModel
    {
        public static JsonInitialiser JsonTranslator = new JsonInitialiser();

        public static ObservableCollection<Employee> Employees { get; set; }

        public static int CurrentID = 0;

        public ICommand OpenEmployeeAddWindow {  get; set; }
        public ICommand OpenEmployeeShiftWindow { get; set; }

        public ICommand RemoveEmployee { get; set; }

        public MainViewModel()
        {
            JsonTranslator.CheckFileExistance();
            Employees = JsonTranslator.LoadEmployees(50);
            var shifts = JsonTranslator.LoadShift();
            var employeesById = Employees.ToDictionary(e => e.Id);
            foreach (var shift in shifts)
            {
                if (employeesById.TryGetValue(shift.EmployeeId, out var employee))
                {
                    employee.Shifts.Add(shift);
                }
            }
            OpenEmployeeAddWindow = new CommunicateCommand(ExecuteEmployeeAddWindow, CanOpenEmployeeAddWindow);
            OpenEmployeeShiftWindow = new CommunicateCommand(ExecuteEmployeeShiftWindow, CanOpenEmployeeShiftWindow);
            RemoveEmployee = new CommunicateCommand(ExecuteEmployeeRemoval, CanRemoveEmployee);
        }



        public static void WriteEmployeeFile(string jsonData)
        {
            JsonTranslator.WriteEmployeeFile(jsonData);
        }

        public static void WriteShiftFile()
        {
            JsonTranslator.WriteShiftFile();
        }

        public static void AddEmployee(Employee employee)
        {
            employee.Id = CurrentID;
            ++CurrentID;

            Employees.Add(employee);
        }

        private bool CanRemoveEmployee(object obj)
        {
            if(obj == null)
            {  
                return false; 
            }
            else
            {
                return true;
            }
        }

        private void ExecuteEmployeeRemoval(object obj)
        {
            _ = JsonTranslator.DeleteEmployeeById(((Employee)obj).Id);

            Employees.Remove((Employee)obj);
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
