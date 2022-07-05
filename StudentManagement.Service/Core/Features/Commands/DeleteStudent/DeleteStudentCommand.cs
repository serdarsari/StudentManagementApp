using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.StudentDTO;
using StudentManagement.Service.Core.IConfiguration;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Features.Commands.DeleteStudent
{
    public class DeleteStudentCommand : IRequest<DeleteStudentResponse>
    {
        public int StudentId { get; set; }
        public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, DeleteStudentResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILoggerService _loggerService;

            public DeleteStudentCommandHandler(IUnitOfWork unitOfWork, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _loggerService = loggerService;
            }
            public async Task<DeleteStudentResponse> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var student = await _unitOfWork.Students.GetByIdAsync(request.StudentId);
                    if (student == null)
                    {
                        _loggerService.Log("DeleteStudentAsync invalid studentId attempt.", CustomLogLevel.Warning);
                        return new DeleteStudentResponse { IsSuccess = false, Message = "ERROR: Geçersiz 'studentId' bilgisi girdiniz." };
                    }

                    _unitOfWork.Students.Delete(student);
                    await _unitOfWork.CompleteAsync();

                    return new DeleteStudentResponse { IsSuccess = true, Message = "Silme işlemi başarılı!" };
                }
                catch (DbUpdateException dbex)
                {
                    _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                    return new DeleteStudentResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                    return new DeleteStudentResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
                }
            }
        }
    }
}
