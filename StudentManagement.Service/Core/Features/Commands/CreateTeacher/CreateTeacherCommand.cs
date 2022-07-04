using MediatR;
using StudentManagement.DTO.TeacherDTO;

namespace StudentManagement.Service.Core.Features.Commands.CreateTeacher
{
    public partial class CreateTeacherCommand : IRequest<CreateTeacherResponse>
    {
        public int LessonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Profession { get; set; }
        public string? Description { get; set; }
    }
}
