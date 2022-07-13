using MediatR;
using StudentManagement.DTO.StudentDTO;

namespace StudentManagement.Service.Core.Features.Commands.CreateStudent
{
    public partial class CreateStudentCommand : IRequest<CreateStudentResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmergencyCall { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int Grade { get; set; }
        public string ClassBranch { get; set; }
		public string Email { get; set; }
	}
}
