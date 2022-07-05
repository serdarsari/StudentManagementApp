using MediatR;
using StudentManagement.DTO.LessonDTO;

namespace StudentManagement.Service.Core.Features.Commands.CreateLesson
{
    public partial class CreateLessonCommand : IRequest<CreateLessonResponse>
    {
        public string LessonCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
