using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.LessonDTO;
using StudentManagement.Entity;

namespace StudentManagement.Service.LessonService
{
    public class LessonService : ILessonService
    {
        private readonly StudentManagementAppDbContext _dbContext;

        public LessonService(StudentManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<CreateLessonResponse> CreateLessonAsync(CreateLessonRequest request)
        {
            try
            {
                var lesson = new Lesson
                {
                    LessonCode = request.LessonCode,
                    Name = request.Name,
                    Description = request.Description,
                };

                await _dbContext.AddAsync(lesson);
                await _dbContext.SaveChangesAsync();

                return new CreateLessonResponse { IsSuccess = true, Message = "Ders oluşturma işlemi başarılı!" };
            }
            catch (DbUpdateException dbex)
            {
                return new CreateLessonResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu." };
            }
            catch (Exception ex)
            {
                return new CreateLessonResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }

        }
    }
}
