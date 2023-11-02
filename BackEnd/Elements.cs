using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BackEnd {
    public class Employer {
        [XmlElement]
        public string? CompanyName;
        [XmlArray]
        public List<Employee>? Employees;
    }

    public class Employee {
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
    }

    public class AddressElement {
        [XmlElement]
        public string? Street;
        [XmlElement]
        public int? StreetNo;
        [XmlElement]
        public string? City;
    }
}
