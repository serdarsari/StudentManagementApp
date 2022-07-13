using MediatR;
using StudentManagement.DTO.UserDTO;

namespace StudentManagement.Service.Core.Features.Commands.CreateUser
{
    public partial class CreateUserCommand : IRequest<CreateUserResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
