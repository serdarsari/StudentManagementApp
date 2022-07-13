using MediatR;
using StudentManagement.DTO.UserDTO;

namespace StudentManagement.Service.Core.Features.Commands.CreateToken
{
    public partial class CreateTokenCommand : IRequest<CreateTokenResponse>
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string UserType { get; set; }
	}
}
