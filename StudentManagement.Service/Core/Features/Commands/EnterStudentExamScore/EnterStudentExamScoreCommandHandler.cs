using MediatR;
using StudentManagement.DTO.ExamProcedureDTO;
using StudentManagement.Service.Core.IConfigurationRepository;

namespace StudentManagement.Service.Core.Features.Commands.EnterStudentExamScore
{
    public partial class EnterStudentExamScoreCommand
    {
        public class EnterStudentExamScoreCommandHandler : IRequestHandler<EnterStudentExamScoreCommand, EnterStudentExamScoreResponse>
        {
            private readonly IUnitOfWork _unitOfWork;

            public EnterStudentExamScoreCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<EnterStudentExamScoreResponse> Handle(EnterStudentExamScoreCommand request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.ExamProcedures.EnterStudentExamScoreAsync(request);
            }
        }
    }
}
