using System.Xml.Serialization;

namespace BackEnd {
    public class Employer : IComparable<Employer> {
        [XmlElement]
        public string CompanyName { get; set; } = string.Empty;
        [XmlArray]
        public List<Employee> Employees { get; set; } = new();

        public int CompareTo(Employer? other) {
            if (other == null)
                return 1;
            if (CompanyName == null)
                return -1;
            else
                return CompanyName.CompareTo(other.CompanyName);
        }

        public void Sort() {
            Employees.Sort();
        }
    }
}
