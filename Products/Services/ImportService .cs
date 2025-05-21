using Products.Interfaces;

namespace Products.Services
{
    public class ImportService<T> : IImportService<T> where T : class
    {
        private readonly IFileDownloader _downloader;
        private readonly ICsvReader<T> _reader;
        private readonly IFilterFactory<T> _filterFactory;
        private readonly IRepository<T> _repository;

        public ImportService(IFileDownloader downloader, ICsvReader<T> reader, IFilterFactory<T> filterFactory, IRepository<T> repository)
        {
            _downloader = downloader;
            _reader = reader;
            _filterFactory = filterFactory;
            _repository = repository;
        }

        public async Task ImportAsync(string url, string localFilePath)
        {
            await _downloader.DownloadFileAsync(url, localFilePath);

            var entities = _reader.Read(localFilePath);
            var filter = await _filterFactory.CreateAsync();
            var filtered = filter.Filter(entities);
            await _repository.SaveAsync(filtered);
        }
    }
}