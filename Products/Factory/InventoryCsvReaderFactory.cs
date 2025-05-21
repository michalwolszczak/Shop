using Products.CsvReaders;
using Products.Entities;
using Products.Interfaces;

namespace Products.Factory
{
    public class InventoryCsvReaderFactory : ICsvReaderFactory<Inventory>
    {
        public ICsvReader<Inventory> Create(string filePath)
        {
            return new InventoryCsvReader(filePath);
        }
    }
}
