using System.Linq.Expressions;

namespace StudentManagement.Service.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> AddAsync(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        Task<T> FirstOrDefaultAsync();
        Task<int> MaxAsync(Expression<Func<T, int>> selector);
        Task<int> CountAsync();
    }
}
