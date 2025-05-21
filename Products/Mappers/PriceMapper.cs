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
            Map(m => m.NettPrice).Index(2);
            Map(m => m.NettPriceAfterDiscount).Index(3);
            Map(m => m.VatRate).Index(4).Convert(row =>
            {
                var value = row.Row.GetField(4);
                return decimal.TryParse(value, out var result) ? result : 0m;
            });
            Map(m => m.NettPriceAfterDiscountPerUnit).Index(5);
        }
    }
}