using Products.CsvReaders;
using Products.Entities;
using Products.Interfaces;

namespace Products.Factory
{
    public class ProductCsvReaderFactory : ICsvReaderFactory<Product>
    {
        public ICsvReader<Product> Create(string filePath)
        {
            return new ProductCsvReader(filePath);
        }
    }
}