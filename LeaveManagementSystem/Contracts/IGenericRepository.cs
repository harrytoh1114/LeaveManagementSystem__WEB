namespace LeaveManagementSystem.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int? id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<bool> Exists(int id);  
        Task DeletAsync(int id);
        Task UpdateAsync(T entity);
    }
}
