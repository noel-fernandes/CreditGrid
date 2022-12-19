namespace CreditGrid.Notifier.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task AddEntityAsync(T entity);

        int Complete();

        Task<int> CompleteAsync();
    }
}
