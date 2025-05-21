using Products.Entities;
using Products.Interfaces;
using System.Globalization;
using System.Text;
using CsvHelper;

namespace Products.CsvReaders
{
    public class InventoryCsvReader : ICsvReader<Inventory>
    {
        private readonly StreamReader _reader;
        private readonly CsvReader _csv;

        public InventoryCsvReader(string path)
        {
            _reader = new StreamReader(path, Encoding.UTF8, true);
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                DetectDelimiter = true,
                MissingFieldFound = null,
                HeaderValidated = null,
                BadDataFound = null,
            };
            _csv = new CsvReader(_reader, config);
        }

        public IEnumerable<Inventory> Read(string path)
        {
            foreach (var record in _csv.GetRecords<Inventory>())
            {
                yield return record;
            }
        }

        public void Dispose()
        {
            _csv.Dispose();
            _reader.Dispose();
        }
    }
}