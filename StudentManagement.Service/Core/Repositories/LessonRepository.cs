using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.LessonDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.Features.Queries.GetLessons;
using StudentManagement.Service.Core.Features.Queries.GetLessonsByTeacher;
using StudentManagement.Service.Core.IRepositories;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Repositories
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(StudentManagementAppDbContext dbContext, ILoggerService loggerService) : base(dbContext, loggerService)
        {
        }

        public async Task<GetLessonsByTeacherResponse> GetLessonsByTeacher(GetLessonsByTeacherQuery request)
        {
            try
            {
                var lessons = await _dbContext.Teachers.Where(x => x.Id == request.TeacherId)
                    .Join(_dbContext.Lessons, sc => sc.LessonId, soc => soc.Id, (sc, soc) => new { sc, soc })
                    .Select(s => s.soc).ToListAsync();

                var currentStartRow = (request.PageNumber - 1) * request.PageSize;
                var response = new GetLessonsByTeacherResponse
                {
                    NextPage = $"api/Lessons?PageNumber={request.PageNumber + 1}&PageSize={request.PageSize}",
                    TotalLessons = lessons.Count(),
                };

                var responseLessons = lessons.Skip(currentStartRow).Take(request.PageSize)
                    .Select(t => new LessonResponse
                    {
                        Id = t.Id,
                        LessonCode = t.LessonCode,
                        Name = t.Name,
                        Description = t.Description
                    }).ToList();

                response.Lessons = responseLessons;
                return response;
            }
            catch (Exception ex)
            {
                _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                return new GetLessonsByTeacherResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }
    }
}
