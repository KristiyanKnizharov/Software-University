namespace ProductShop.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("CategoryProduct")]
    public class ImportCategoryProductDto
    {
        [XmlElement("categoryId")]
        public int CategoryId { get; set; }

        [XmlElement("productId")]
        public int ProductId { get; set; }
    }
}
