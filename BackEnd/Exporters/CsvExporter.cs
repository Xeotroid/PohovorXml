using CsvHelper;
using CsvHelper.Configuration;
using System.Text;
using System.Globalization;

namespace BackEnd {
    internal class CsvExporter : IExporter {
        public void SaveTo(List<Employer> inputList, string outputPath) {
            using FileStream fs = new(outputPath, FileMode.OpenOrCreate);
            using StreamWriter writer = new (fs, Encoding.UTF8);
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture) {
                Delimiter = ";",
            };
            using var csv = new CsvWriter(writer, csvConfig);

            csv.Context.RegisterClassMap<CsvEmployeeMap>();

            foreach (Employer emp in inputList) {
                csv.WriteRecords(emp.Employees);
            }
        }
    }
}
