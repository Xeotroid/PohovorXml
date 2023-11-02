using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BackEnd {
    public class Employer : IComparable<Employer> {
        [XmlElement]
        public string? CompanyName { get; set; }
        [XmlArray]
        public List<Employee>? Employees { get; set; }

        public int CompareTo(Employer? other) {
            if (other == null)
                return 1;
            if (CompanyName == null)
                return -1;
            else
                return CompanyName.CompareTo(other.CompanyName);
        }
    }

    public class Employee : IComparable<Employee> {
        static readonly log4net.ILog _log = LogHelper.GetLogger();
        [XmlElement]
        public int Id { get; set; }
        [XmlElement]
        public string? FirstName { get; set; }
        [XmlElement]
        public string? LastName { get; set; }
        [XmlElement]
        public AddressElement? Address { get; set; }
        [XmlElement]
        public string? EmployedSince { get; set; }

        public Employer? ParentEmployer { get; set; }

        public int GetEmployeeNumber() {
            return Id;
        }

        public string GetFullName() {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName)) {
                _log.Error($"Jméno nebo příjmení zaměstnance {Id} není uvedeno.");
                return "CHYBA";
            }

            string first = FirstName.FirstUpper();
            string last = LastName.FirstUpper();
            return $"{last} {first}";
        }

        public string GetEmployedSince() {
            //TODO: Změnit na parsování a reformátování datetime stringu
            if(DateTime.TryParse(EmployedSince, out DateTime dt)) {
                return dt.ToString("yyyy-MM-dd");
            }
            _log.Error($"EmployedSince zaměstnance {Id} je ve špatném formátu.");
            return "CHYBA";
        }

        public int CompareTo(Employee? other) {
            if (other == null)
                return 1;
            else
                return GetFullName().CompareTo(other.GetFullName());
        }
    }

    public class AddressElement {
        static readonly log4net.ILog _log = LogHelper.GetLogger();
        [XmlElement]
        public string? Street { get; set; }
        [XmlElement]
        public int? StreetNo { get; set; }
        [XmlElement]
        public string? City { get; set; }

        public Employee? ParentEmployee;

        public string GetFullAddress() {
            if (string.IsNullOrEmpty(City)) {
                if(ParentEmployee == null) {
                    _log.Error("Není uvedeno město neznámého zaměstnance.");
                } else {
                    _log.Error($"Není uvedeno město zaměstnance {ParentEmployee.Id}.");
                }                
                return "CHYBA";
            }
            if (string.IsNullOrEmpty(Street)) {
                return City;
            }
            if (StreetNo is null) {
                return $"{Street}, {City}";
            }
            return $"{Street} {StreetNo}, {City}";
        }
    }
}
