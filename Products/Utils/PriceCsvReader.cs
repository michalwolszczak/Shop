using CsvHelper;
using Products.Entities;
using Products.Interfaces;
using Products.Mappers;
using System.Globalization;
using System.Text;

namespace Products.Utils
{
    public class PriceCsvReader : ICsvReader<Price>
    {
        public IEnumerable<Price> Read(string path)
        {
            using var reader = new StreamReader(path, Encoding.UTF8, true);
            var config = new CsvHelper.Configuration.CsvConfiguration(new CultureInfo("pl-PL"))
            {
                MissingFieldFound = null,
                HeaderValidated = null,
                BadDataFound = null,
                IgnoreBlankLines = true,
                HasHeaderRecord = false,
                DetectDelimiter = true,
                ShouldSkipRecord = ShouldSkipRecord
            };

            using var csv = new CsvReader(reader, config);

            csv.Context.RegisterClassMap<PriceMapper>();

            return csv.GetRecords<Price>().ToList();
        }

        private bool ShouldSkipRecord(ShouldSkipRecordArgs args)
        {
            if (args.Row.Parser.Record == null)
                return true;

            return args.Row.Parser.Record.All(string.IsNullOrWhiteSpace);
        }

    }
}
