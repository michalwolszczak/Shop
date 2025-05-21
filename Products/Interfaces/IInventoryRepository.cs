namespace Products.Interfaces
{
    public interface IInventoryRepository<T> : IRepository<T>
    {
        Task<HashSet<string>> GetAllSKUs();
    }
}
