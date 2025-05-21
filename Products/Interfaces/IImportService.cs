namespace Products.Interfaces
{
    public interface IImportService<T> where T : class
    {
        Task ImportAsync(string url, string localFilePath);
    }
}
