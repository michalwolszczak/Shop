namespace Products.Dto
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string EAN { get; set; }
        public string ProducerName { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public decimal NettPrice { get; set; }
        public decimal ShippingCost { get; set; }
    }
}