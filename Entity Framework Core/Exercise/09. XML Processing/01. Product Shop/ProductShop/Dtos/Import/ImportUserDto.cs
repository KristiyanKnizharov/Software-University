namespace ProductShop.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("User")]
    public class ImportUserDto
    {
        //<firstName>Chrissy</firstName>
        //<lastName>Falconbridge</lastName>
        //<age>50</age>

        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int Age { get; set; }

    }
}
