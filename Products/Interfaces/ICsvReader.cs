namespace Products.Interfaces
{
    public interface ICsvReader<T> : IDisposable
    {
        IEnumerable<T> Read(string path);
    }
}