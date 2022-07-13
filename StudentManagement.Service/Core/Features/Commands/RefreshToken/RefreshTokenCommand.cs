using AutoMapper;
using MediatR;
using StudentManagement.DTO.UserDTO;

namespace StudentManagement.Service.Core.Features.Commands.RefreshToken
{
    public partial class RefreshTokenCommand : IRequest<RefreshTokenResponse>
    {
        public string RefreshToken { get; set; }
    }
}
