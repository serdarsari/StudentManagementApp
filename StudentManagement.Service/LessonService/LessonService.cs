using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.LessonDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LogService;

namespace StudentManagement.Service.LessonService
{
    public class LessonService : ILessonService
    {
        private readonly StudentManagementAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        public LessonService(StudentManagementAppDbContext dbContext, IMapper mapper, ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<CreateLessonResponse> CreateLessonAsync(CreateLessonRequest request)
        {
            try
            {
                var lesson = _mapper.Map<Lesson>(request);

                await _dbContext.Lessons.AddAsync(lesson);
                await _dbContext.SaveChangesAsync();

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
