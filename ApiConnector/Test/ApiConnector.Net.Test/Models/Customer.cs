using System;
using System.Xml.Serialization;

namespace ApiConnector.Net.Test.Models
{
    [Serializable, XmlRoot("customer")]
    public class Customer
    {
        [XmlElement(ElementName = "id")]
        public int Id { get; set; }

        [XmlElement(ElementName = "firstName")]
        public string FirstName { get; set; }

        [XmlElement(ElementName = "lastName")]
        public string LastName { get; set; }
    }
}
