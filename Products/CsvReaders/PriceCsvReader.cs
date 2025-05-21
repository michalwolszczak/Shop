using CsvHelper;
using Products.Entities;
using Products.Interfaces;
using Products.Mappers;
using System.Globalization;
using System.Text;

namespace Products.CsvReaders
{
    public class PriceCsvReader : ICsvReader<Price>
    {
        private readonly StreamReader _reader;
        private readonly CsvReader _csv;

        public PriceCsvReader(string path)
        {
            _reader = new StreamReader(path, Encoding.UTF8, true);
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null,
                HeaderValidated = null,
                BadDataFound = null,
                IgnoreBlankLines = true,
                HasHeaderRecord = false,
                DetectDelimiter = true,
                ShouldSkipRecord = ShouldSkipRecord
            };

            _csv = new CsvReader(_reader, config);
            _csv.Context.RegisterClassMap<PriceMapper>();
        }

        public IEnumerable<Price> Read(string path)
        {
            foreach (var record in _csv.GetRecords<Price>())
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

            return args.Row.Parser.Record.All(string.IsNullOrWhiteSpace);
        }
    }
}
