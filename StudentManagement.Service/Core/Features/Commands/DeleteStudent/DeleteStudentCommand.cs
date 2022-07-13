using MediatR;
using StudentManagement.DTO.StudentDTO;

namespace StudentManagement.Service.Core.Features.Commands.DeleteStudent
{
    public partial class DeleteStudentCommand : IRequest<DeleteStudentResponse>
    {
        public int StudentId { get; set; }
    }
}
