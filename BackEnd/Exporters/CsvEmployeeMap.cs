using CsvHelper.Configuration;

namespace BackEnd {
    internal class CsvEmployeeMap : ClassMap<Employee> {
        public CsvEmployeeMap() {
            Map(m => m.ParentEmployer.CompanyName).Index(0).Name("CompanyName");
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
