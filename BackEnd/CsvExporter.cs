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
        public bool SaveTo(List<Employer> inputList, string outputPath) {
            using var writer = new StreamWriter(outputPath);
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture) {
                Delimiter = ";",
            };
            using var csv = new CsvWriter(writer, csvConfig);

            csv.Context.RegisterClassMap<EmployeeMap>();

            foreach (Employer emp in inputList) {
                csv.WriteRecords(emp.Employees);
            }
            return true;
        }
    }

    public class EmployeeMap : ClassMap<Employee> {
        public EmployeeMap() {
            Map(m => m.Company.CompanyName).Index(0).Name("CompanyName");
            //tohle není pěkné, ale nevidím důvod, proč poměrně velké operace
            //se stringy převádět do atributů s gettery.
            //Getter je stejnak jen metoda, proč je tohle vůbec potřeba?
            Map(m => m.LastName).Index(1).Name("EmployeeName").Convert(m => m.Value.GetFullName());
            Map(m => m.Id).Index(2).Name("EmployeeNumber");
            Map(m => m.Address.City).Index(3).Name("EmployeeAddress").Convert(m => m.Value.Address.GetFullAddress());
            Map(m => m.EmployedSince).Index(4).Name("EmployedSince").Convert(m => m.Value.GetEmployedSince());
        }
    }
}
