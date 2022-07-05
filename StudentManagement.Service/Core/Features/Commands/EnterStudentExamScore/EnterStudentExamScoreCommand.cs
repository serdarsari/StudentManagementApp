using MediatR;
using StudentManagement.DTO.ExamProcedureDTO;

namespace StudentManagement.Service.Core.Features.Commands.EnterStudentExamScore
{
    public partial class EnterStudentExamScoreCommand : IRequest<EnterStudentExamScoreResponse>
    {
        public int LessonId { get; set; }
        public int StudentId { get; set; }
        public int ExamScore { get; set; }
    }
}
