using StudentManagement.Service.Core.IRepositories;

namespace StudentManagement.Service.Core.IConfigurationRepository
{
    public interface IUnitOfWork
    {
        ITeacherRepository Teachers { get; }
        IStudentRepository Students { get; }
        IParentRepository Parents { get; }
        IManagerRepository Managers { get; }
        ILessonRepository Lessons { get; }
        IExamProcedureRepository ExamProcedures { get; }
        IUserRepository Users { get; }
        ILessonStudentRepository LessonStudent { get; }

        Task CompleteAsync();
    }
}
