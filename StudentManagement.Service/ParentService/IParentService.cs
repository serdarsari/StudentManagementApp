using StudentManagement.DTO.ParentDTO;

namespace StudentManagement.Service.ParentService
{
    public interface IParentService
    {
        Task<CreateParentResponse> CreateParentAsync(CreateParentRequest request);
        Task<AssignSingleStudentToParentResponse> AssignSingleStudentToParentAsync(AssignSingleStudentToParentRequest request);
    }
}
