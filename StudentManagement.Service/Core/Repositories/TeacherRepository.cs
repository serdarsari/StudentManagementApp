using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.TeacherDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.Features.Commands.AssignMultipleStudentToTeacher;
using StudentManagement.Service.Core.IRepositories;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(StudentManagementAppDbContext dbContext, ILoggerService loggerService) : base(dbContext, loggerService)
        {
        }

        public async Task<AssignMultipleStudentToTeacherResponse> AssignMultipleStudentToTeacherAsync(AssignMultipleStudentToTeacherCommand request)
        {
            try
            {
                var teacher = await _dbContext.Teachers.SingleOrDefaultAsync(t => t.Id == request.TeacherId);
                if (teacher == null)
                    return new AssignMultipleStudentToTeacherResponse { IsSuccess = false, Message = "ERROR: Geçersiz 'TeacherId' bilgisi girdiniz." };

                List<StudentTeacher> studentTeacherList = new List<StudentTeacher>();

                foreach (var studentId in request.StudentIds)
                {
                    var studentTeacher = new StudentTeacher
                    {
                        StudentId = studentId,
                        TeacherId = request.TeacherId,
                    };
                    studentTeacherList.Add(studentTeacher);
                }

                await _dbContext.StudentTeacher.AddRangeAsync(studentTeacherList);
                await _dbContext.SaveChangesAsync();

                return new AssignMultipleStudentToTeacherResponse { IsSuccess = true, Message = "Atama işlemi başarılı!" };
            }
            catch (DbUpdateException dbex)
            {
                _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                return new AssignMultipleStudentToTeacherResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
            }
            catch (Exception ex)
            {
                _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                return new AssignMultipleStudentToTeacherResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }
    }
}
