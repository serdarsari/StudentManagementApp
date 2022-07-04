using MediatR;
using StudentManagement.DTO.TeacherDTO;

namespace StudentManagement.Service.Core.Features.Commands.AssignMultipleStudentToTeacher
{
    public partial class AssignMultipleStudentToTeacherCommand : IRequest<AssignMultipleStudentToTeacherResponse>
    {
        public int TeacherId { get; set; }
        public List<int> StudentIds { get; set; }
    }
}
