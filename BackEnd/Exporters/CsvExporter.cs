using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BackEnd {
    internal class CsvExporter : IExporter {
        public void SaveTo(List<Employer> inputList, string outputPath) {
            FileStream fs = new(outputPath, FileMode.OpenOrCreate);
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
