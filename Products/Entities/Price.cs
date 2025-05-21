using CsvHelper.Configuration.Attributes;

namespace Products.Entities
{
    public class Price
    {
        public string Id { get; set; }
        public string Sku { get; set; }
        public decimal? NettPriceAfterDiscountPerUnit { get; set; }
    }
}