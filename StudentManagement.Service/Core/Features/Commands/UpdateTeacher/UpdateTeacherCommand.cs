using MediatR;
using StudentManagement.DTO.TeacherDTO;

namespace StudentManagement.Service.Core.Features.Commands.UpdateTeacher
{
    public partial class UpdateTeacherCommand : IRequest<UpdateTeacherResponse>
    {
        public int TeacherId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Profession { get; set; }
        public string Description { get; set; }
    }
}
