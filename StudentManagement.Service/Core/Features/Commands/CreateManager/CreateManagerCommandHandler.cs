using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.ManagerDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Common;
using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Features.Commands.CreateManager
{
    public partial class CreateManagerCommand
    {
        public class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, CreateManagerResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ILoggerService _loggerService;

            public CreateManagerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _loggerService = loggerService;
            }
            public async Task<CreateManagerResponse> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var lastId = 0;
                    var checkIfDatabaseIsEmpty = await _unitOfWork.Managers.FirstOrDefaultAsync();
                    if (checkIfDatabaseIsEmpty != null)
                        lastId = await _unitOfWork.Managers.MaxAsync(t => t.Id);

                    var newId = lastId + 1;
                    string newRegistrationNumber = RegistrationNumberGenerator.Create(RegistrationNumberTypes.MNGR, newId);

                    var manager = _mapper.Map<Manager>(request);
                    manager.RegistrationNumber = newRegistrationNumber;

                    await _unitOfWork.Managers.AddAsync(manager);
                    await _unitOfWork.CompleteAsync();

                    return new CreateManagerResponse { IsSuccess = true, Message = "Oluşturma işlemi başarılı!" };
                }
                catch (DbUpdateException dbex)
                {
                    _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                    return new CreateManagerResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                    return new CreateManagerResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
                }
            }
        }
    }
}
