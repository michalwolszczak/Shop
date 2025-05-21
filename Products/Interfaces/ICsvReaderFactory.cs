namespace Products.Interfaces
{
    public interface ICsvReaderFactory<T>
    {
        ICsvReader<T> Create(string filePath);
    }
}