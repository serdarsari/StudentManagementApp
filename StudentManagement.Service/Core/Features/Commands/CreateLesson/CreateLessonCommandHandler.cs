using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.LessonDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Features.Commands.CreateLesson
{
    public partial class CreateLessonCommand
    {
        public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, CreateLessonResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ILoggerService _loggerService;

            public CreateLessonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _loggerService = loggerService;
            }
            public async Task<CreateLessonResponse> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var lesson = _mapper.Map<Lesson>(request);

                    await _unitOfWork.Lessons.AddAsync(lesson);
                    await _unitOfWork.CompleteAsync();

                    return new CreateLessonResponse { IsSuccess = true, Message = "Oluşturma işlemi başarılı!" };
                }
                catch (DbUpdateException dbex)
                {
                    _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                    return new CreateLessonResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                    return new CreateLessonResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
                }
            }
        }
    }
}
