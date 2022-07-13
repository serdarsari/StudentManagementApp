using MediatR;
using StudentManagement.DTO.StudentDTO;

namespace StudentManagement.Service.Core.Features.Queries.GetStudentsByLesson
{
    public partial class GetStudentsByLessonQuery : IRequest<GetStudentsByLessonResponse>
    {
        public int LessonId { get; set; }
    }
}
