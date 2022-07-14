using StudentManagement.DTO.StudentDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.Features.Queries.GetstudentsByTeacher;
using StudentManagement.Service.Core.Features.Queries.GetStudentsForrAssignmentToTeacher;

namespace StudentManagement.Service.Core.IRepositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<GetStudentsByTeacherResponse> GetStudentsByTeacher(GetStudentsByTeacherQuery request);
        Task<GetStudentsForAssignmentToTeacherResponse> GetStudentsForAssignmentToTeacher(GetStudentsForAssignmentToTeacherQuery request);
    }
}
