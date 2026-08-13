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
using System.Security.Cryptography;

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

        public async Task DeleteEmployeeById(int id)
        {
            await WipeEmployeeShifts(id);

            string tempFile = EmployeeFile + ".tmp";

            using var input = new StreamReader(EmployeeFile);
            await using var output = new StreamWriter(tempFile, false);

            string? line;

            while ((line = await input.ReadLineAsync()) != null)
            {
                var deserialisedLine = JsonConvert.DeserializeObject<Employee>(line);

                if (deserialisedLine != null && deserialisedLine.Id == id)
                    continue;

                await output.WriteLineAsync(line);
            }

            input.Close();
            output.Close();

            File.Move(tempFile, EmployeeFile, true);
        }

        private async Task WipeEmployeeShifts(int id)
        {
            string tempFile = ShiftFile + ".tmp";

            using var input = new StreamReader(ShiftFile);
            await using var output = new StreamWriter(tempFile, false);

            string? line;

            while ((line = await input.ReadLineAsync()) != null)
            {
                var deserialisedLine = JsonConvert.DeserializeObject<Shift>(line);

                if (deserialisedLine != null && deserialisedLine.EmployeeId == id)
                    continue;

                await output.WriteLineAsync(line);
            }

            input.Close();
            output.Close();

            File.Move(tempFile, ShiftFile, true);
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

            foreach (var shift in allShifts)
            {
                File.AppendAllText(ShiftFile, JsonConvert.SerializeObject(shift) + Environment.NewLine);
            }
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
