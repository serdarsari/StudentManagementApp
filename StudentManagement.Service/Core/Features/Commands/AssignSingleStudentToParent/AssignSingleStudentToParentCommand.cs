using MediatR;
using StudentManagement.DTO.ParentDTO;

namespace StudentManagement.Service.Core.Features.Commands.AssignSingleStudentToParent
{
    public partial class AssignSingleStudentToParentCommand : IRequest<AssignSingleStudentToParentResponse>
    {
        public int ParentId { get; set; }
        public int StudentId { get; set; }
    }
}
