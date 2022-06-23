using StudentManagement.DTO.TeacherDTO;

namespace StudentManagement.Service.TeacherService
{
    public interface ITeacherService
    {
        Task<GetTeacherDetailResponse> GetTeacherDetailAsync(int teacherId);
        Task<CreateTeacherResponse> CreateTeacherAsync(CreateTeacherRequest request);
        Task<DeleteTeacherResponse> DeleteTeacherAsync(int teacherId);
        Task<UpdateTeacherResponse> UpdateTeacherAsync(int teacherId, UpdateTeacherRequest request);
    }
}
