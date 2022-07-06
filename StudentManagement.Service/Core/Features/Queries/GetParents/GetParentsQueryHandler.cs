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
                var currentStartRow = (request.PageNumber - 1) * request.PageSize;
                var response = new GetParentsResponse
                {
                    NextPage = $"api/Parents?PageNumber={request.PageNumber + 1}&PageSize={request.PageSize}",
                    TotalParents = await _unitOfWork.Parents.CountAsync(),
                };

                var parents = await _unitOfWork.Parents.GetAllAsync();

                var responseParents = parents.Skip(currentStartRow).Take(request.PageSize)
                    .Select(s => new ParentResponse
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        PhoneNumber = s.PhoneNumber
                        
                    }).ToList();

                response.Parents = responseParents;
                return response;
            }
        }
    }
}
