using MediatR;
using StudentManagement.DTO.BaseClasses;
using StudentManagement.DTO.ParentDTO;

namespace StudentManagement.Service.Core.Features.Queries.GetParents
{
    public partial class GetParentsQuery : GetAllBaseRequest, IRequest<GetParentsResponse>
    {
    }
}
