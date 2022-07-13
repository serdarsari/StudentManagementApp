using MediatR;
using StudentManagement.DTO.ParentDTO;
using StudentManagement.Service.Core.IConfigurationRepository;

namespace StudentManagement.Service.Core.Features.Queries.GetParentsByTeacher
{
    public partial class GetParentsByTeacherQuery
    {
        public class GetParentsByTeacherQueryHandler : IRequestHandler<GetParentsByTeacherQuery, GetParentsByTeacherResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            public GetParentsByTeacherQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<GetParentsByTeacherResponse> Handle(GetParentsByTeacherQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Parents.GetParentsByTeacher(request);
            }
        }
    }
}
