using StudentManagement.DTO.StudentDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.Features.Queries.GetStudentsByLesson;
using StudentManagement.Service.Core.IRepositories;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Repositories
{
    public class LessonStudentRepository : GenericRepository<LessonStudent>, ILessonStudentRepository
    {
        public LessonStudentRepository(StudentManagementAppDbContext dbContext, ILoggerService loggerService) : base(dbContext, loggerService)
        {
        }

        public GetStudentsByLessonResponse GetStudentsByLesson(GetStudentsByLessonQuery query)
        {
            try
            {
                var students = _dbContext.LessonStudent.Where(s => s.LessonId == query.LessonId)
                    .Join(_dbContext.Students, sc => sc.StudentId, soc => soc.Id, (sc, soc) => new { Student = soc })
                    .Select(s => new StudentByLessonResponse{ 
                        Id = s.Student.Id,
                        FullName = s.Student.FirstName + " " + s.Student.LastName
                    }).ToList();

                var response = new GetStudentsByLessonResponse
                {
                    Students = students
                };

                return response;
            }
            catch (Exception ex)
            {
                _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                return new GetStudentsByLessonResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }
    }
}
