using StudentManagement.DTO.StudentDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.Features.Queries.GetStudentsByLesson;

namespace StudentManagement.Service.Core.IRepositories
{
    public interface ILessonStudentRepository : IGenericRepository<LessonStudent>
    {
        GetStudentsByLessonResponse GetStudentsByLesson(GetStudentsByLessonQuery query);
    }
}
