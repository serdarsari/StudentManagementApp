using StudentManagement.DTO.TeacherDTO;

namespace StudentManagement.Service.TeacherService
{
    public interface ITeacherService
    {
        Task<GetTeachersResponse> GetTeachersAsync(GetTeachersRequest request);
        Task<GetTeacherDetailResponse> GetTeacherDetailAsync(int teacherId);
        Task<CreateTeacherResponse> CreateTeacherAsync(CreateTeacherRequest request);
        Task<DeleteTeacherResponse> DeleteTeacherAsync(int teacherId);
        Task<UpdateTeacherResponse> UpdateTeacherAsync(int teacherId, UpdateTeacherRequest request);
        Task<AssignMultipleStudentToTeacherResponse> AssignMultipleStudentToTeacherAsync(AssignMultipleStudentToTeacherRequest request);
        Task<AssignSingleStudentToTeacherResponse> AssignSingleStudentToTeacherAsync(AssignSingleStudentToTeacherRequest request);
    }
}
