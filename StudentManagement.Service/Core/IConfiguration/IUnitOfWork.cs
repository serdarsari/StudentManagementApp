using StudentManagement.Service.Core.IRepositories;

namespace StudentManagement.Service.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        ITeacherRepository Teachers { get; }
        IStudentRepository Students { get; }
        IParentRepository Parents { get; }
        IManagerRepository Managers { get; }
        ILessonRepository Lessons { get; }
        IExamProcedureRepository ExamProcedures { get; }

        Task CompleteAsync();
    }
}
