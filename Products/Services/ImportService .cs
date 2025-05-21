using Products.Interfaces;

namespace Products.Services
{
    public class ImportService<T> : IImportService<T> where T : class
    {
        private readonly IFileDownloader _downloader;
        private readonly ICsvReaderFactory<T> _readerFactory;
        private readonly IFilterFactory<T> _filterFactory;
        private readonly IRepository<T> _repository;

        public ImportService(IFileDownloader downloader, ICsvReaderFactory<T> readerFactory, IFilterFactory<T> filterFactory, IRepository<T> repository)
        {
            _downloader = downloader;
            _readerFactory = readerFactory;
            _filterFactory = filterFactory;
            _repository = repository;
        }

        public async Task ImportAsync(string url, string localFilePath)
        {
            await _downloader.DownloadFileAsync(url, localFilePath);

            //problem with memory, beacause of ToList()
            using var reader = _readerFactory.Create(localFilePath);
            var entities = reader.Read(localFilePath);            
            var filter = await _filterFactory.CreateAsync();
            var filtered = filter.Filter(entities);
            await _repository.SaveAsync(filtered);
        }
    }
}