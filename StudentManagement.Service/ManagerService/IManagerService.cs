using StudentManagement.DTO.ManagerDTO;

namespace StudentManagement.Service.ManagerService
{
    public interface IManagerService
    {
        Task<CreateManagerResponse> CreateManagerAsync(CreateManagerRequest request);
    }
}
