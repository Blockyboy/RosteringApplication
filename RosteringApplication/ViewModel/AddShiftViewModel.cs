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
using System.Text.Json;
using System.IO;

namespace RosteringApplication.ViewModel
{
    public class AddShiftViewModel
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime Start { get; set; } = DateTime.Now;

        private DateTime _end;
        public DateTime End 
        { 
            get => _end;
            
            set
            {
                if(Minimum != null && value < Minimum)
                {
                    value = (DateTime)Minimum;
                }
                else if(Maximum != null && value > Maximum)
                {
                    value = (DateTime)Maximum;
                }

                _end = value;
            } 
        }
        public string? Description { get; set; }
        public ICommand AddShift { get; set; }
        public DateTime? Minimum =>
        MultiDay ? null : Start;

        public DateTime? Maximum =>
        MultiDay ? null : Date.Date + new TimeSpan(23, 59, 0);
        public bool MultiDay { get; set; }
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
            Shift shift = new Shift(DateOnly.FromDateTime(Date), TimeOnly.FromDateTime(Start), TimeOnly.FromDateTime(End), Description, Parent.Employee.Id);
            Parent.AddShift(shift);
            MainViewModel.WriteShiftFile();
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
