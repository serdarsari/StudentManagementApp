using MediatR;
using StudentManagement.DTO.StudentDTO;

namespace StudentManagement.Service.Core.Features.Commands.UpdateStudent
{
    public partial class UpdateStudentCommand : IRequest<UpdateStudentResponse>
    {
        public int StudentId { get; set; }
        public string EmergencyCall { get; set; }
        public string Address { get; set; }
        public string ClassBranch { get; set; }
        public double GPA { get; set; }
    }
}
