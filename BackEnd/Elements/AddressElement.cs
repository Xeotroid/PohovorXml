using System.Xml.Serialization;

namespace BackEnd
{
    public class AddressElement
    {
        static readonly log4net.ILog _log = LogHelper.GetLogger();
        [XmlElement]
        public string Street { get; set; } = string.Empty;
        [XmlElement]
        //Sice velice silně pochybuji, že někde existuje č.p./č.o. 0,
        //ale pro jistotu nechám nulu jako validní.
        public int? StreetNo { get; set; }
        [XmlElement]
        public string City { get; set; } = string.Empty;

        public Employee? ParentEmployee;

        /// <summary>
        /// Vrátí úplnou adresu podle dostupných údajů.
        /// </summary>
        /// <returns>Platná adresa, při nedostatečných údajích "CHYBA".</returns>
        public string GetFullAddress()
        {
            if (City == "")
            {
                if (ParentEmployee == null)
                {
                    _log.Error("Není uvedeno město neznámého zaměstnance.");
                }
                else
                {
                    _log.Error($"Není uvedeno město zaměstnance {ParentEmployee.Id}.");
                }
                return "CHYBA";
            }
            if (Street == "")
            {
                return City;
            }
            if (StreetNo is null)
            {
                return $"{Street}, {City}";
            }
            return $"{Street} {StreetNo}, {City}";
        }
    }
}
