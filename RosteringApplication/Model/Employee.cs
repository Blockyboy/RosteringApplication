using RosteringApplication.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RosteringApplication.Model
{
    public class Employee
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public ObservableCollection<Shift> Shifts { get; set; } = new();

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public Employee(string firstName, string lastName, string role)
        {
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }

        public void AddShift(Shift shift)
        {
           Shifts.Add(shift);
        }

    }
}
