using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosteringApplication.Model
{
    public class Employee
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

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

    }
}
