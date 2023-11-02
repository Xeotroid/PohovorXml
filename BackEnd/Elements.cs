using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BackEnd {
    public class Employer : IComparable<Employer> {
        [XmlElement]
        public string? CompanyName;
        [XmlArray]
        public List<Employee>? Employees;

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
        [XmlElement]
        public int Id;
        [XmlElement]
        public string? FirstName;
        [XmlElement]
        public string? LastName;
        [XmlElement]
        public AddressElement? Address;
        [XmlElement]
        public string? EmployedSince;

        public int GetEmployeeNumber() {
            return Id;
        }

        public string GetFullName() {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName)) {
                throw new NullReferenceException("Jmeno nebo prijmeni je prazdne!");
            }

            string first = FirstName.FirstUpper();
            string last = LastName.FirstUpper();
            return $"{first} {last}";
        }

        public string GetEmployedSince() {
            //TODO: Změnit na parsování a reformátování datetime stringu
            return EmployedSince.Substring(0, 10);
        }

        public int CompareTo(Employee? other) {
            if (other == null)
                return 1;
            else
                return GetFullName().CompareTo(other.GetFullName());
        }
    }

    public class AddressElement {
        [XmlElement]
        public string? Street;
        [XmlElement]
        public int? StreetNo;
        [XmlElement]
        public string? City;

        public string GetFullAddress() {
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
