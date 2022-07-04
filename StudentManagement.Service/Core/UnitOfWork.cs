using StudentManagement.Entity;
using StudentManagement.Service.Core.IConfiguration;
using StudentManagement.Service.Core.IRepositories;
using StudentManagement.Service.Core.Repositories;
using StudentManagement.Service.LoggerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Service.Core
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly StudentManagementAppDbContext _dbContext;
        private readonly ILoggerService _loggerService;

        public ITeacherRepository Teachers { get; private set; }

        public UnitOfWork(StudentManagementAppDbContext dbContext, ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _loggerService = loggerService;

            Teachers = new TeacherRepository(_dbContext, _loggerService);
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
