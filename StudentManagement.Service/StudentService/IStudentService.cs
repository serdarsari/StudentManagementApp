using StudentManagement.DTO.StudentDTO;

namespace StudentManagement.Service.StudentService
{
    public interface IStudentService
    {
        Task<GetStudentsResponse> GetStudentsAsync(GetStudentsRequest request);
        Task<GetStudentDetailResponse> GetStudentDetailAsync(int studentId);
        Task<CreateStudentResponse> CreateStudentAsync(CreateStudentRequest request);
        Task<DeleteStudentResponse> DeleteStudentAsync(int studentId);
        Task<UpdateStudentResponse> UpdateStudentAsync(UpdateStudentRequest request);
    }
}
