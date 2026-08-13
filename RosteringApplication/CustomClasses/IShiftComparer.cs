using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RosteringApplication.Model;

namespace RosteringApplication.CustomClasses
{
    class IShiftComparer : IEqualityComparer<Shift>
    {
        public bool Equals(Shift? shift1, Shift? shift2)
        {
            if(shift1.Date == shift2.Date)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(Shift? shift)
        {
            return shift.GetHashCode();
        }
    }
}
