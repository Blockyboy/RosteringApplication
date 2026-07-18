using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosteringApplication.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public Employee(int id, string firstName, string lastName, string role)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }

    }
}
