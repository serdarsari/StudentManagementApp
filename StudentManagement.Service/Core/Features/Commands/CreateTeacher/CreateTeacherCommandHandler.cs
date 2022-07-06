using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.TeacherDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Common;
using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Features.Commands.CreateTeacher
{
    public partial class CreateTeacherCommand
    {
        public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, CreateTeacherResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ILoggerService _loggerService;

            public CreateTeacherCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _loggerService = loggerService;
            }
            public async Task<CreateTeacherResponse> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var lastId = 0;
                    var checkIfDatabaseIsEmpty = await _unitOfWork.Teachers.FirstOrDefaultAsync();
                    
                    if (checkIfDatabaseIsEmpty != null)
                        lastId = await _unitOfWork.Teachers.MaxAsync(t => t.Id);

                    var newId = lastId + 1;
                    string newRegistrationNumber = RegistrationNumberGenerator.Create(RegistrationNumberTypes.TCHR, newId);

                    var teacher = _mapper.Map<Teacher>(request);
                    teacher.RegistrationNumber = newRegistrationNumber;

                    await _unitOfWork.Teachers.AddAsync(teacher);
                    await _unitOfWork.CompleteAsync();

                    return new CreateTeacherResponse { IsSuccess = true, Message = "Oluşturma işlemi başarılı!" };
                }
                catch (DbUpdateException dbex)
                {
                    _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                    return new CreateTeacherResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                    return new CreateTeacherResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
                }
            }
        }
    }
}
