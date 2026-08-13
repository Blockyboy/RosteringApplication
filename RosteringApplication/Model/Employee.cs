using RosteringApplication.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;
using RosteringApplication.CustomClasses;

namespace RosteringApplication.Model
{
    public class Employee : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public ObservableSet<Shift> Shifts { get; set; } = new(new IShiftComparer());

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public DateOnly? NextShift
        {
            get { 
                    if (Shifts.Count > 0)
                    {
                        return Shifts.Max(x => x.Date);
                    }

                return null;
                }
        }

        public Employee(string firstName, string lastName, string role)
        {
            FirstName = firstName;
            LastName = lastName;
            Role = role;

            Shifts.CollectionChanged += Shifts_CollectionChanged;
        }

        private void Shifts_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(NextShift));
        }

        public void AddShift(Shift shift)
        {
            if ((Shifts.Select(x => x.Date)).Contains(shift.Date))
            {
                MessageBox.Show(
                    "The employee already has a shift on this date",
                    "Date Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
            else
            {
                Shifts.Add(shift);
            }
        }

    }
}
