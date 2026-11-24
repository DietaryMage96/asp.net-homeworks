namespace Dll.Repository
{
    public interface IRepository<T, TKey>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(TKey id);
        Task Add(T data);
        Task Update(T data);
        Task Delete(T data);
    }
}
