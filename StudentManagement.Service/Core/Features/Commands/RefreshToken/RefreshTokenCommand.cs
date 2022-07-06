using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using StudentManagement.DTO.UserDTO;
using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.LoggerService;
using StudentManagement.Service.TokenService;

namespace StudentManagement.Service.Core.Features.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
    {
        public string RefreshToken { get; set; }
        public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ILoggerService _loggerService;
            private readonly IConfiguration _configuration;

            public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService, IConfiguration configuration)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _loggerService = loggerService;
                _configuration = configuration;
            }
            public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken && u.RefreshTokenExpireDate > DateTime.Now);
                    if (user == null)
                        return new RefreshTokenResponse { IsSuccess = false, Message = "Oturum sonlandı, tekrar giriş yapın." };

                    var handler = new TokenHandler(_configuration);
                    Token token = handler.CreateAccessToken(user);
                    user.RefreshToken = token.RefreshToken;
                    user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                    _unitOfWork.Users.Update(user);
                    await _unitOfWork.CompleteAsync();

                    return new RefreshTokenResponse { IsSuccess = true, AccessToken = token.AccessToken, Expiration = token.Expiration, RefreshToken = token.RefreshToken };
                }
                catch (Exception)
                {
                    return new RefreshTokenResponse { IsSuccess = false, Message = "Error." };
                }
            }
        }
    }
}
