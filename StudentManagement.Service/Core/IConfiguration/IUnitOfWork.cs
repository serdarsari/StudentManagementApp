using StudentManagement.Service.Core.IRepositories;

namespace StudentManagement.Service.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        ITeacherRepository Teachers { get; }

        Task CompleteAsync();
    }
}
