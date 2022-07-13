using MediatR;
using StudentManagement.DTO.ParentDTO;
using StudentManagement.Service.Core.IConfigurationRepository;

namespace StudentManagement.Service.Core.Features.Queries.GetParents
{
    public partial class GetParentsQuery
    {
        public class GetParentsQueryHandler : IRequestHandler<GetParentsQuery, GetParentsResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            public GetParentsQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<GetParentsResponse> Handle(GetParentsQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Parents.GetParentsWithChild(request);
            }
        }
    }
}
