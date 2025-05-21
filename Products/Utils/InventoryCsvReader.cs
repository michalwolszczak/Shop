using CsvHelper;
using Products.Entities;
using Products.Interfaces;
using System.Globalization;
using System.Text;

namespace Products.Utils
{
    public class InventoryCsvReader : ICsvReader<Inventory>
    {
        public IEnumerable<Inventory> Read(string path)
        {
            using var reader = new StreamReader(path, Encoding.UTF8, true);
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                DetectDelimiter = true,
                MissingFieldFound = null,
                HeaderValidated = null,
                BadDataFound = null,
            };
            using var csv = new CsvReader(reader, config);
            return csv.GetRecords<Inventory>().ToList();
        }
    }
}