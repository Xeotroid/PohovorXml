using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BackEnd
{
    public class AddressElement
    {
        static readonly log4net.ILog _log = LogHelper.GetLogger();
        [XmlElement]
        public string Street { get; set; } = string.Empty;
        [XmlElement]
        public int? StreetNo { get; set; }
        [XmlElement]
        public string City { get; set; } = string.Empty;

        public Employee? ParentEmployee;

        public string GetFullAddress()
        {
            if (string.IsNullOrEmpty(City))
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
            if (string.IsNullOrEmpty(Street))
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
