using MediatR;
using StudentManagement.DTO.ExamProcedureDTO;

namespace StudentManagement.Service.Core.Features.Queries.GetExamResultsByStudent
{
    public partial class GetExamResultsByStudentQuery : IRequest<GetExamResultsByStudentResponse>
	{
		public int StudentId { get; set; }
	}
}
