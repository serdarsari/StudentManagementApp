using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.StudentDTO;
using StudentManagement.Service.Core.IConfiguration;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Features.Commands.UpdateStudent
{
    public class UpdateStudentCommand : IRequest<UpdateStudentResponse>
    {
        public int StudentId { get; set; }
        public string EmergencyCall { get; set; }
        public string Address { get; set; }
        public string ClassBranch { get; set; }
        public double GPA { get; set; }

        public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, UpdateStudentResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ILoggerService _loggerService;

            public UpdateStudentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _loggerService = loggerService;
            }
            public async Task<UpdateStudentResponse> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var student = await _unitOfWork.Students.GetByIdAsync(request.StudentId);
                    if (student == null)
                    {
                        _loggerService.Log("UpdateStudentAsync invalid studentId attempt.", CustomLogLevel.Warning);
                        return new UpdateStudentResponse { IsSuccess = false, Message = "ERROR: Geçersiz 'StudentId' bilgisi girdiniz." };
                    }

                    student.EmergencyCall = request.EmergencyCall != student.EmergencyCall ? request.EmergencyCall : student.EmergencyCall;
                    student.Address = request.Address != student.Address ? request.Address : student.Address;
                    student.ClassBranch = request.ClassBranch != student.ClassBranch ? request.ClassBranch : student.ClassBranch;
                    student.GPA = request.GPA != student.GPA ? request.GPA : student.GPA;

                    _unitOfWork.Students.Update(student);
                    await _unitOfWork.CompleteAsync();

                    return new UpdateStudentResponse { IsSuccess = true, Message = "Güncelleme işlemi başarılı!" };
                }
                catch (DbUpdateException dbex)
                {
                    _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                    return new UpdateStudentResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                    return new UpdateStudentResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
                }
            }
        }
    }
}
