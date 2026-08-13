using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using RosteringApplication.Model;
using System.Collections.ObjectModel;
using RosteringApplication.ViewModel;

namespace RosteringApplication
{
    public class JsonInitialiser
    {
        private static readonly string EmployeeFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RosteringApplication", "EmployeeData.json");

        private static readonly string ShiftFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RosteringApplication", "ShiftData.json");

        public ObservableCollection<Employee> LoadEmployees(int count)
        {
            var employees = new ObservableCollection<Employee>();

            using var stream = File.OpenRead(EmployeeFile);
            using var reader = new StreamReader(stream);

            while (employees.Count < count)
            {
                var line = reader.ReadLine();

                if (line == null)
                    break;

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var employee =
                    JsonConvert.DeserializeObject<Employee>(line);

                if (employee != null)
                    employees.Add(employee);
            }

            reader.Close();

            return employees;
        }

        public List<Shift> LoadShift()
        {
            return JsonConvert.DeserializeObject<List<Shift>>(File.ReadAllText(ShiftFile)) ?? [];
        }

        public void WriteEmployeeFile(string jsonData)
        {
            File.AppendAllText(EmployeeFile, jsonData + Environment.NewLine);
        }

        public void WriteShiftFile()
        {
            var allShifts = MainViewModel.Employees.SelectMany(e => e.Shifts).ToList();

            File.WriteAllText(ShiftFile, JsonConvert.SerializeObject(allShifts) + Environment.NewLine);
        }

        public void CheckFileExistance()
        {
            if (!File.Exists(EmployeeFile))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(EmployeeFile)!);
                File.WriteAllText(EmployeeFile, string.Empty);
            }

            if (!File.Exists(ShiftFile))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(ShiftFile)!);
                File.WriteAllText(ShiftFile, string.Empty);
            }
        }

    }
}
