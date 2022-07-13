using MediatR;
using StudentManagement.DTO.ExamProcedureDTO;
using StudentManagement.Service.Core.IConfigurationRepository;

namespace StudentManagement.Service.Core.Features.Queries.GetExamResultsByStudent
{
    public partial class GetExamResultsByStudentQuery
    {
        public class GetExamResultsByStudentHandler : IRequestHandler<GetExamResultsByStudentQuery, GetExamResultsByStudentResponse>
		{
			private readonly IUnitOfWork _unitOfWork;
			public GetExamResultsByStudentHandler(IUnitOfWork unitOfWork)
			{
				_unitOfWork = unitOfWork;
			}
			public async Task<GetExamResultsByStudentResponse> Handle(GetExamResultsByStudentQuery request, CancellationToken cancellationToken)
			{
				return await _unitOfWork.ExamProcedures.GetExamResultsByStudent(request);
			}
		}
	}
}
