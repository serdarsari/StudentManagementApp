using StudentManagement.DTO.StudentDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.Features.Queries.GetstudentsByTeacher;

namespace StudentManagement.Service.Core.IRepositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<GetStudentsByTeacherResponse> GetStudentsByTeacher(GetStudentsByTeacherQuery request);
    }
}
