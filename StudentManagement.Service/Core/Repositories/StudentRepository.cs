using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.StudentDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.Features.Queries.GetstudentsByTeacher;
using StudentManagement.Service.Core.Features.Queries.GetStudentsForrAssignmentToTeacher;
using StudentManagement.Service.Core.IRepositories;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(StudentManagementAppDbContext dbContext, ILoggerService loggerService) : base(dbContext, loggerService)
        {
        }

        public async Task<GetStudentsByTeacherResponse> GetStudentsByTeacher(GetStudentsByTeacherQuery request)
        {
            try
            {
                var students = await _dbContext.StudentTeacher.Where(x => x.TeacherId == request.TeacherId)
                    .Join(_dbContext.Students, sc => sc.StudentId, soc => soc.Id, (sc, soc) => new { sc, soc })
                    .Select(s => s.soc).ToListAsync();

                var currentStartRow = (request.PageNumber - 1) * request.PageSize;
                var response = new GetStudentsByTeacherResponse
                {
                    NextPage = $"api/Students?PageNumber={request.PageNumber + 1}&PageSize={request.PageSize}",
                    TotalStudents = students.Count(),
                };

                var responseStudents = students.Skip(currentStartRow).Take(request.PageSize)
                    .Select(s => new StudentResponse
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Grade = s.Grade,
                        ClassBranch = s.ClassBranch,
                        GPA = s.GPA,
                        Gender = s.Gender
                    }).ToList();

                response.Students = responseStudents;
                return response;
            }
            catch (Exception ex)
            {
                _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                return new GetStudentsByTeacherResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }

        public async Task<GetStudentsForAssignmentToTeacherResponse> GetStudentsForAssignmentToTeacher(GetStudentsForAssignmentToTeacherQuery request)
        {
            try
            {
                var notIncludedStudentIds =  await _dbContext.StudentTeacher.Where(x => x.TeacherId == request.TeacherId)
                    .Select(x => x.StudentId).ToListAsync();

                var students = await _dbContext.StudentTeacher.Where(x => !notIncludedStudentIds.Contains(x.StudentId))
                    .Join(_dbContext.Students, sc => sc.StudentId, soc => soc.Id, (sc, soc) => new { sc, soc })
                    .Select(x => x.soc).ToListAsync();

                var currentStartRow = (request.PageNumber - 1) * request.PageSize;
                var response = new GetStudentsForAssignmentToTeacherResponse
                {
                    NextPage = $"api/Students?PageNumber={request.PageNumber + 1}&PageSize={request.PageSize}",
                    TotalStudents = students.Count(),
                };

                var responseStudents = students.Skip(currentStartRow).Take(request.PageSize)
                    .Select(s => new StudentResponse
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Grade = s.Grade,
                        ClassBranch = s.ClassBranch,
                        GPA = s.GPA,
                        Gender = s.Gender
                    }).ToList();

                response.Students = responseStudents;
                return response;
            }
            catch (Exception ex)
            {
                _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                return new GetStudentsForAssignmentToTeacherResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }
    }
}
