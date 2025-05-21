namespace Products.Interfaces
{
    public interface IRepository<T>
    {
        Task SaveAsync(IEnumerable<T> entities);
    }
}
