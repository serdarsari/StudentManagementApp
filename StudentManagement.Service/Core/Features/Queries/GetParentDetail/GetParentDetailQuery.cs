using MediatR;
using StudentManagement.DTO.ParentDTO;

namespace StudentManagement.Service.Core.Features.Queries.GetParentDetail
{
    public partial class GetParentDetailQuery : IRequest<GetParentDetailResponse>
    {
        public int ParentId { get; set; }
    }
}
