using Microsoft.Extensions.Configuration;
using StudentManagement.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly StudentManagementAppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public UserService(StudentManagementAppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
    }
}
