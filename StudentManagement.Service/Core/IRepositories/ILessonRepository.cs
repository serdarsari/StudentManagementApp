using StudentManagement.DTO.LessonDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.Features.Queries.GetLessonsByTeacher;

namespace StudentManagement.Service.Core.IRepositories
{
    public interface ILessonRepository : IGenericRepository<Lesson>
    {
        Task<GetLessonsByTeacherResponse> GetLessonsByTeacher(GetLessonsByTeacherQuery request);
    }
}
