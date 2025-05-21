namespace Products.Interfaces
{
    public interface IFilterFactory<T>
    {
        Task<IFilter<T>> CreateAsync();
    }
}
