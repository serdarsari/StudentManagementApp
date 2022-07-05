using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.StudentDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Common;
using StudentManagement.Service.Core.IConfiguration;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Features.Commands.CreateStudent
{
    public partial class CreateStudentCommand
    {
        public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, CreateStudentResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ILoggerService _loggerService;

            public CreateStudentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _loggerService = loggerService;
            }
            public async Task<CreateStudentResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var lastId = 0;
                    var checkIfDatabaseIsEmpty = await _unitOfWork.Students.FirstOrDefaultAsync();
                    if (checkIfDatabaseIsEmpty != null)
                        lastId = await _unitOfWork.Students.MaxAsync(t => t.Id);

                    var newId = lastId + 1;
                    string newRegistrationNumber = RegistrationNumberGenerator.Create(RegistrationNumberTypes.STDN, newId);

                    var student = _mapper.Map<Student>(request);
                    student.StudentId = newRegistrationNumber;

                    await _unitOfWork.Students.AddAsync(student);
                    await _unitOfWork.CompleteAsync();

                    return new CreateStudentResponse { IsSuccess = true, Message = "Oluşturma işlemi başarılı!" };
                }
                catch (DbUpdateException dbex)
                {
                    _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                    return new CreateStudentResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                    return new CreateStudentResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
                }
            }
        }
    }
}
