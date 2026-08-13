using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosteringApplication.Model
{
    public class Shift : IComparable<Shift> 
    {
        public DateOnly Date {  get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }

        public DateTime? Minimum =>
        MultiDay ? null : Date.ToDateTime(Start);

        public DateTime? Maximum =>
        MultiDay ? null : Date.ToDateTime(new TimeOnly(23, 59));
        public bool MultiDay { get; set;}
        public string? Description { get; set; }

        public int EmployeeId { get; set; }

        public Shift(DateOnly date, TimeOnly start, TimeOnly end, string? description, int id)
        {
            Date = date;
            Start = start;
            End = end;
            Description = description;
            EmployeeId = id;
        }

        public int CompareTo(Shift? other)
        {
            return Date.CompareTo(other?.Date);
        }
    }
}
