using CsvHelper.Configuration;
using Products.Entities;

namespace Products.Mappers
{
    public class PriceMapper : ClassMap<Price>
    {
        public PriceMapper()
        {
            Map(m => m.Id).Index(0);
            Map(m => m.Sku).Index(1);
            Map(m => m.NettPriceAfterDiscountPerUnit).Index(5);
        }
    }
}