using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.LessonDTO;
using StudentManagement.Entity;

namespace StudentManagement.Service.LessonService
{
    public class LessonService : ILessonService
    {
        private readonly StudentManagementAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public LessonService(StudentManagementAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CreateLessonResponse> CreateLessonAsync(CreateLessonRequest request)
        {
            try
            {
                var lesson = _mapper.Map<Lesson>(request);

                await _dbContext.AddAsync(lesson);
                await _dbContext.SaveChangesAsync();

                return new CreateLessonResponse { IsSuccess = true, Message = "Oluşturma işlemi başarılı!" };
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
