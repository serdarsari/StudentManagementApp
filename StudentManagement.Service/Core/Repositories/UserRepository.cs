using StudentManagement.Entity;
using StudentManagement.Service.Core.IRepositories;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(StudentManagementAppDbContext dbContext, ILoggerService loggerService) : base(dbContext, loggerService)
        {
        }
    }
}
