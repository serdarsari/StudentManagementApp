using StudentManagement.DTO.ParentDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.Features.Commands.AssignSingleStudentToParent;

namespace StudentManagement.Service.Core.IRepositories
{
    public interface IParentRepository : IGenericRepository<Parent>
    {
        Task<AssignSingleStudentToParentResponse> AssignSingleStudentToParentAsync(AssignSingleStudentToParentCommand request);
    }
}
