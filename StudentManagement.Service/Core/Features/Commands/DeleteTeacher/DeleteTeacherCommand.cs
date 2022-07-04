using MediatR;
using StudentManagement.DTO.TeacherDTO;

namespace StudentManagement.Service.Core.Features.Commands.DeleteTeacher
{
    public partial class DeleteTeacherCommand : IRequest<DeleteTeacherResponse>
    {
        public int TeacherId { get; set; }
    }
}
