using MediatR;
using Microsoft.Extensions.Configuration;
using StudentManagement.DTO.UserDTO;
using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;
using StudentManagement.Service.TokenService;

namespace StudentManagement.Service.Core.Features.Commands.CreateToken
{
    public partial class CreateTokenCommand
    {
        public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, CreateTokenResponse>
		{
			private readonly IUnitOfWork _unitOfWork;
            private readonly ILoggerService _loggerService;
            private readonly IConfiguration _configuration;

			public CreateTokenCommandHandler(IUnitOfWork unitOfWork, ILoggerService loggerService, IConfiguration configuration)
			{
				_unitOfWork = unitOfWork;
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

					int id = 0;
					if (request.UserType == "Student")
					{
						var ids = await _unitOfWork.Students.FirstOrDefaultAsync(x => x.Email == request.Email);
						if (ids == null)
							return new CreateTokenResponse { IsSuccess = false, Message = "Hesabınızın türünü doğru seçin lütfen." };
						id = ids.Id;
					}
                    else if (request.UserType == "Teacher")
                    {
                        var ids = await _unitOfWork.Teachers.FirstOrDefaultAsync(x => x.Email == request.Email);
						if (ids == null)
							return new CreateTokenResponse { IsSuccess = false, Message = "Hesabınızın türünü doğru seçin lütfen." };
						id = ids.Id;
                    }
					else if (request.UserType == "Admin")
					{
						var ids = await _unitOfWork.Managers.FirstOrDefaultAsync(x => x.Email == request.Email);
                        if (ids == null)
							return new CreateTokenResponse { IsSuccess=false,Message="Hesabınızın türünü doğru seçin lütfen."};
                        id = ids.Id;
                    }
					else
                    {
						return new CreateTokenResponse { IsSuccess = false, Message = "Geçersiz Rol bilgisi!" };
					}

					TokenHandler handler = new TokenHandler(_configuration);
					var token = handler.CreateAccessToken(user);

					user.RefreshToken = token.RefreshToken;
					user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
					_unitOfWork.Users.Update(user);
					await _unitOfWork.CompleteAsync();

					return new CreateTokenResponse
					{
						IsSuccess = true,
						AccessToken = token.AccessToken,
						Expiration = token.Expiration,
						RefreshToken = token.RefreshToken,
						FirstName = user.FirstName,
						LastName = user.LastName,
						Role = user.Role,
						Id = id
					};
				}
				catch (Exception ex)
				{
					_loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
					return new CreateTokenResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
				}
			}
		}
	}
}
