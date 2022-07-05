using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.ParentDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.IConfiguration;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Features.Commands.CreateParent
{
    public partial class CreateParentCommand
    {
        public class CreateParentCommandHandler : IRequestHandler<CreateParentCommand, CreateParentResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ILoggerService _loggerService;

            public CreateParentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _loggerService = loggerService;
            }
            public async Task<CreateParentResponse> Handle(CreateParentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var parent = _mapper.Map<Parent>(request);
                    await _unitOfWork.Parents.AddAsync(parent);
                    await _unitOfWork.CompleteAsync();

                    return new CreateParentResponse { IsSuccess = true, Message = "Oluşturma işlemi başarılı!" };
                }
                catch (DbUpdateException dbex)
                {
                    _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                    return new CreateParentResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                    return new CreateParentResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
                }
            }
        }
    }
}
