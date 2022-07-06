
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using StudentManagement.DTO.UserDTO;
using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.LoggerService;
using StudentManagement.Service.TokenService;

namespace StudentManagement.Service.Core.Features.Commands.CreateToken
{
    public class CreateTokenCommand : IRequest<CreateTokenResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, CreateTokenResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ILoggerService _loggerService;
            private readonly IConfiguration _configuration;

            public CreateTokenCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService, IConfiguration configuration)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _loggerService = loggerService;
                _configuration = configuration;
            }
            public async Task<CreateTokenResponse> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);
                    if (user == null)
                    {
                        return new CreateTokenResponse { IsSuccess = false, Message = "Kullanıcı adı veya şifre yanlış!" };
                    }

                    TokenHandler handler = new TokenHandler(_configuration);
                    var token = handler.CreateAccessToken(user);

                    user.RefreshToken = token.RefreshToken;
                    user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                    _unitOfWork.Users.Update(user);
                    await _unitOfWork.CompleteAsync();

                    return new CreateTokenResponse { IsSuccess = true, AccessToken = token.AccessToken, Expiration = token.Expiration, RefreshToken = token.RefreshToken };
                }
                catch (Exception)
                {
                    return new CreateTokenResponse { IsSuccess = false, Message = "Error." };
                }
            }
        }
    }
}
