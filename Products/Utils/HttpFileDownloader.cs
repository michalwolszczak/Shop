using Products.Interfaces;

namespace Products.Utils
{
    public class HttpFileDownloader : IFileDownloader
    {
        private readonly HttpClient _httpClient;

        public HttpFileDownloader(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DownloadFileAsync(string url, string destinationPath)
        {
            var data = await _httpClient.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(destinationPath, data);
        }
    }
}