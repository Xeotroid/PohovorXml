using System.Xml.Serialization;

namespace BackEnd
{
    public class Employee : IComparable<Employee> {
        static readonly log4net.ILog _log = LogHelper.GetLogger();
        [XmlElement]
        public int Id { get; set; }
        [XmlElement]
        public string FirstName { get; set; } = string.Empty;
        [XmlElement]
        public string LastName { get; set; } = string.Empty;
        [XmlElement]
        public AddressElement Address { get; set; } = new();
        [XmlElement]
        public string EmployedSince { get; set; } = string.Empty;

        public Employer? ParentEmployer { get; set; }

        public bool IsUnemployed => string.IsNullOrEmpty(EmployedSince);

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
}
