namespace Products.Interfaces
{
    public interface ICsvReader<T> where T : class
    {
        IEnumerable<T> Read(string path);
    }
}