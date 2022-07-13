using MediatR;
using StudentManagement.DTO.BaseClasses;
using StudentManagement.DTO.ParentDTO;

namespace StudentManagement.Service.Core.Features.Queries.GetParentsByTeacher
{
    public partial class GetParentsByTeacherQuery : GetAllBaseRequest, IRequest<GetParentsByTeacherResponse>
    {
        public int TeacherId { get; set; }
    }
}
