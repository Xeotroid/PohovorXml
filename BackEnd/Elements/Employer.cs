using System.Xml.Serialization;

namespace BackEnd {
    public class Employer : IComparable<Employer> {
        [XmlElement]
        public string CompanyName { get; set; } = string.Empty;
        [XmlArray]
        public List<Employee> Employees { get; set; } = new();

        /// <summary>
        /// Porovnání s ostatními zaměstnavateli podle názvu.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Employer? other) {
            if (other == null)
                return 1;
            if (CompanyName == string.Empty)
                return -1;
            else
                return CompanyName.CompareTo(other.CompanyName);
        }

        public void Sort() {
            Employees.Sort();
        }

        public void RemoveAll(Predicate<Employee> predicate) => Employees?.RemoveAll(predicate);
    }
}
