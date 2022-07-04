using StudentManagement.DTO.TeacherDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.Features.Commands.AssignMultipleStudentToTeacher;

namespace StudentManagement.Service.Core.IRepositories
{
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        Task<AssignMultipleStudentToTeacherResponse> AssignMultipleStudentToTeacherAsync(AssignMultipleStudentToTeacherCommand request);
    }
}
