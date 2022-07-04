using Microsoft.EntityFrameworkCore;
using StudentManagement.Entity;
using StudentManagement.Service.Core.IRepositories;
using StudentManagement.Service.LoggerService;
using System.Linq.Expressions;

namespace StudentManagement.Service.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected StudentManagementAppDbContext _dbContext;
        protected readonly ILoggerService _loggerService;
        protected DbSet<T> dbSet;

        public GenericRepository(StudentManagementAppDbContext dbContext, ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _loggerService = loggerService;
            dbSet= dbContext.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
        public virtual async Task<bool> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual bool Update(T entity)
        {
            dbSet.Update(entity);
            return true;
        }
        public bool Delete(T entity)
        {
            dbSet.Remove(entity);
            return true;
        }

        public virtual async Task<T> FirstOrDefaultAsync()
        {
            return await dbSet.FirstOrDefaultAsync();
        }

        public virtual async Task<int> MaxAsync(Expression<Func<T, int>> selector)
        {
            return await dbSet.MaxAsync(selector);
        }

        public async Task<int> CountAsync()
        {
            return await dbSet.CountAsync();
        }
    }
}
