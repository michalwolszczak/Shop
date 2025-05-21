namespace Products.Interfaces
{
    public interface IFileDownloader
    {
        Task DownloadFileAsync(string url, string destinationPath);
    }
}