using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.UserDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Service.Core.Features.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ILoggerService _loggerService;

            public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _loggerService = loggerService;
            }
            public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await _unitOfWork.Users.SingleOfDefaultAsync(u => u.Email == request.Email);
                    if (user != null)
                        return new CreateUserResponse { IsSuccess = false, Message = "Aynı Email bilgisine sahip başka kullanıcı zaten mevcut." };

                    user = _mapper.Map<User>(request);

                    //BUNU KALDIR TESTTEN SONRA
                    user.RefreshToken = "aaa";

                    await _unitOfWork.Users.AddAsync(user);
                    await _unitOfWork.CompleteAsync();

                    return new CreateUserResponse { IsSuccess = true, Message = "Oluşturma işlemi başarılı!" };
                }
                catch (DbUpdateException dbex)
                {
                    _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                    return new CreateUserResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                    return new CreateUserResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
                }
            }
        }
    }
}
