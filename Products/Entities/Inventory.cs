using CsvHelper.Configuration.Attributes;

namespace Products.Entities
{
    public class Inventory
    {
        [Name("product_id")]
        public int ProductId { get; set; }
        [Name("sku")]
        public string Sku { get; set; }
        [Name("unit")]
        public string Unit { get; set; }
        [Name("qty")]
        public double Quantity { get; set; }
        [Name("manufacturer_name")]
        public string Manufacturer { get; set; }
        [Name("shipping")]
        public string Shipping { get; set; }
        [Name("shipping_cost")]
        public decimal? ShippingCost { get; set; }
    }
}