using MediatR;
using StudentManagement.DTO.ParentDTO;
using StudentManagement.Service.Core.IConfigurationRepository;

namespace StudentManagement.Service.Core.Features.Commands.AssignSingleStudentToParent
{
    public partial class AssignSingleStudentToParentCommand
    {
        public class AssignSingleStudentToParentCommandHandler : IRequestHandler<AssignSingleStudentToParentCommand, AssignSingleStudentToParentResponse>
        {
            private readonly IUnitOfWork _unitOfWork;

            public AssignSingleStudentToParentCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<AssignSingleStudentToParentResponse> Handle(AssignSingleStudentToParentCommand request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Parents.AssignSingleStudentToParentAsync(request);
            }
        }
    }
}
