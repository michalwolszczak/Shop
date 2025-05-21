using CsvHelper;
using Products.Entities;
using Products.Interfaces;
using System.Globalization;
using System.Text;

namespace Products.Utils
{
    public class ProductCsvReader : ICsvReader<Product>
    {
        public IEnumerable<Product> Read(string path)
        {
            using var reader = new StreamReader(path, Encoding.UTF8, true);
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                DetectDelimiter = true,
                MissingFieldFound = null,
                HeaderValidated = null,
                BadDataFound = null,
                ShouldSkipRecord = ShouldSkipRecord
            };
            using var csv = new CsvHelper.CsvReader(reader, config);
            return csv.GetRecords<Product>().ToList();
        }

        private bool ShouldSkipRecord(ShouldSkipRecordArgs args)
        {
            if (args.Row.Parser.Record == null)
                return true;

            //edge case in Producs, consider creating more SOLID solution
            if (args.Row.Parser.Record.Length == 1 && args.Row.Parser.Record[0] == "__empty_line__")
                return true;

            return args.Row.Parser.Record.All(string.IsNullOrWhiteSpace);
        }
    }
}
