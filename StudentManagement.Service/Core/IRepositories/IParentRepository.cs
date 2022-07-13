using StudentManagement.DTO.ParentDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.Features.Commands.AssignSingleStudentToParent;
using StudentManagement.Service.Core.Features.Queries.GetParents;
using StudentManagement.Service.Core.Features.Queries.GetParentsByTeacher;

namespace StudentManagement.Service.Core.IRepositories
{
    public interface IParentRepository : IGenericRepository<Parent>
    {
        Task<AssignSingleStudentToParentResponse> AssignSingleStudentToParentAsync(AssignSingleStudentToParentCommand request);
        Task<GetParentsResponse> GetParentsWithChild(GetParentsQuery request);
        Task<GetParentsByTeacherResponse> GetParentsByTeacher(GetParentsByTeacherQuery request);
    }
}
