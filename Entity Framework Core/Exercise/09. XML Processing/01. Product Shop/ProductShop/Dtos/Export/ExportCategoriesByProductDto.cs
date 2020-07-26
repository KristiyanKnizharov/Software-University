using System.Xml.Serialization;

namespace ProductShop.Dtos.Export
{
    [XmlType("Product")]
    public class ExportCategoriesByProductDto
    {
        //<Category>
        //    <name>Adult</name>
        //    <count>22</count>
        //    <averagePrice>704.41</averagePrice>
        //    <totalRevenue>15497.02</totalRevenue>
        //</Category>

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlElement("averagePrice")]
        public decimal AveragePrice { get; set; }

        [XmlElement("totalRevenue")]
        public decimal TotalRevenue { get; set; }
    }

    [XmlType("CategoryProducts")]
    public class CategoryProductMappingTable
    {
        [XmlElement("categoryId")]
        public int CategoryId { get; set; }

        [XmlElement("productId")]
        public int ProductId { get; set; }
    }
}
