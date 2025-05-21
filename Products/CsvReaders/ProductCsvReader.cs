using CsvHelper;
using Products.Entities;
using Products.Interfaces;
using System.Globalization;
using System.Text;

namespace Products.CsvReaders
{
    public class ProductCsvReader : ICsvReader<Product>
    {
        private readonly StreamReader _reader;
        private readonly CsvReader _csv;

        public ProductCsvReader(string path)
        {
            _reader = new StreamReader(path, Encoding.UTF8, true);
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                DetectDelimiter = true,
                MissingFieldFound = null,
                HeaderValidated = null,
                BadDataFound = null,
                ShouldSkipRecord = ShouldSkipRecord
            };

            _csv = new CsvReader(_reader, config);
        }

        public IEnumerable<Product> Read(string path)
        {
            foreach(var record in _csv.GetRecords<Product>())
            {
               yield return record;
            }
        }

        public void Dispose()
        {
            _csv.Dispose();
            _reader.Dispose();
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
