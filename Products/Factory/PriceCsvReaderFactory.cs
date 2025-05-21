using Products.CsvReaders;
using Products.Entities;
using Products.Interfaces;

namespace Products.Factory
{
    public class PriceCsvReaderFactory : ICsvReaderFactory<Price>
    {
        public ICsvReader<Price> Create(string filePath)
        {
            return new PriceCsvReader(filePath);
        }
    }
}
