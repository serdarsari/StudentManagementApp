using StudentManagement.DTO.StudentDTO;

namespace StudentManagement.Service.StudentService
{
    public interface IStudentService
    {
        Task<CreateStudentResponse> CreateStudentAsync(CreateStudentRequest request);
    }
}
