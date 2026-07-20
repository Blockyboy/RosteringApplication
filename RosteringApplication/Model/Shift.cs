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
        public string? Description { get; set; }

        public Shift(DateOnly date, TimeOnly start, TimeOnly end, string? description)
        {
            Date = date;
            Start = start;
            End = end;
            Description = description;
        }

        public int CompareTo(Shift? other)
        {
            return Date.CompareTo(other?.Date);
        }
    }
}
