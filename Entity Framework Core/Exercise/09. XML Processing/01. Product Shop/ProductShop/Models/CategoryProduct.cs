namespace ProductShop.Models
{
    using System.Xml.Serialization;

    [XmlType("CategoryProduct")]
    public class CategoryProduct
    {
        [XmlElement("CategoryId")]
        public int CategoryId { get; set; }

        [XmlElement("ProductId")]
        public int ProductId { get; set; }
    }
}
