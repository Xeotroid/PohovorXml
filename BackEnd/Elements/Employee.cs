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

        /// <summary>
        /// Pokud zaměstnanec nemá platnou hodnotu data zaměstnání, je považován za nezaměstnaného.
        /// </summary>
        public bool IsUnemployed => EmployedSince == "";

        /// <summary>
        /// Vrátí ID zaměstnance.
        /// </summary>
        /// <returns></returns>
        public int GetEmployeeNumber() {
            return Id;
        }

        /// <summary>
        /// Vrátí plné jméno zaměstnance se správnými velikostmi písmen.
        /// </summary>
        /// <returns>Plné jméno zaměstnance, při neexistujícím křestním jméně nebo příjmení "CHYBA".</returns>
        public string GetFullName() {
            if (FirstName == "" || LastName == "") {
                _log.Error($"Jméno nebo příjmení zaměstnance {Id} není uvedeno.");
                return "CHYBA";
            }

            string first = FirstName.FirstLettersUpper();
            string last = LastName.FirstLettersUpper();
            return $"{last} {first}";
        }

        /// <summary>
        /// Vrátí datum zaměstnání ve formátu yyyy-MM-dd.
        /// </summary>
        /// <returns>Platný datestring, při neplatném vstupním formátu "CHYBA".</returns>
        public string GetEmployedSince() {
            //TODO: Změnit na parsování a reformátování datetime stringu
            if(DateTime.TryParse(EmployedSince, out DateTime dt)) {
                return dt.ToString("yyyy-MM-dd");
            }
            _log.Error($"EmployedSince zaměstnance {Id} je ve špatném formátu.");
            return "CHYBA";
        }

        /// <summary>
        /// Porovnání s ostatními zaměstnanci podle plného jména (příjmení-křestní)
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Employee? other) {
            if (other == null)
                return 1;
            else
                return GetFullName().CompareTo(other.GetFullName());
        }
    }
}
