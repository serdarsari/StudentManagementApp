using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.TeacherDTO;
using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Features.Commands.DeleteTeacher
{
    public partial class DeleteTeacherCommand
    {
        public class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand, DeleteTeacherResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILoggerService _loggerService;

            public DeleteTeacherCommandHandler(IUnitOfWork unitOfWork, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _loggerService = loggerService;
            }
            public async Task<DeleteTeacherResponse> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var teacher = await _unitOfWork.Teachers.GetByIdAsync(request.TeacherId);
                    if (teacher == null)
                    {
                        _loggerService.Log("DeleteTeacherAsync invalid teacherId attempt.", CustomLogLevel.Warning);
                        return new DeleteTeacherResponse { IsSuccess = false, Message = "ERROR: Geçersiz 'teacherId' bilgisi girdiniz." };
                    }

                    _unitOfWork.Teachers.Delete(teacher);
                    await _unitOfWork.CompleteAsync();

                    return new DeleteTeacherResponse { IsSuccess = true, Message = "Silme işlemi başarılı!" };
                }
                catch (DbUpdateException dbex)
                {
                    _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                    return new DeleteTeacherResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                    return new DeleteTeacherResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
                }
            }
        }
    }
}
