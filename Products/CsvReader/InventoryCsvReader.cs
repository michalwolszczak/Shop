using Products.Entities;
using Products.Interfaces;
using System.Globalization;
using System.Text;

namespace Products.CsvReader
{
    public class InventoryCsvReader : ICsvReader<Inventory>
    {
        public IEnumerable<Inventory> Read(string path)
        {
            using var reader = new StreamReader(path, Encoding.UTF8, true);
            var config = new CsvHelper.Configuration.CsvConfiguration(new CultureInfo("pl-PL"))
            {
                DetectDelimiter = true,
                MissingFieldFound = null,
                HeaderValidated = null,
                BadDataFound = null,
            };
            using var csv = new CsvHelper.CsvReader(reader, config);
            return csv.GetRecords<Inventory>().ToList();
        }
    }
}