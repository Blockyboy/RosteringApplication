using RosteringApplication.Command;
using RosteringApplication.Model;
using RosteringApplication.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RosteringApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace RosteringApplication.ViewModel
{
    public class AddShiftViewModel
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime End { get; set; } = DateTime.Now;
        public string? Description { get; set; }
        public ICommand AddShift { get; set; }

        public EmployeeShiftsViewModel Parent { get; set; }

        public ICommand Cancel { get; set; }

        public AddShiftViewModel(EmployeeShiftsViewModel parent)
        {
            AddShift = new CommunicateCommand(ExecuteShiftAdd, CanAddShift);
            Cancel = new CommunicateCommand(ExecuteCancel, CanCancel);
            Parent = parent;
        }

        private bool CanAddShift(object obj)
        {
            return true;
        }

        private void ExecuteShiftAdd(object obj)
        {
            Shift shift = new Shift(DateOnly.FromDateTime(Date), TimeOnly.FromDateTime(Start), TimeOnly.FromDateTime(End), Description);
            Parent.AddShift(shift);
        }

        private bool CanCancel(object obj)
        {
            return true;
        }

        private void ExecuteCancel(object obj)
        {
            ((Window)obj).Close();
        }
    }
}
