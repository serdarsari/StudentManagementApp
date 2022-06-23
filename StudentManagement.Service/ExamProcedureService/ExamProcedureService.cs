using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.ExamProcedureDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Common;

namespace StudentManagement.Service.ExamProcedureService
{
    public class ExamProcedureService : IExamProcedureService
    {
        private readonly StudentManagementAppDbContext _dbContext;

        public ExamProcedureService(StudentManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EnterStudentExamScoreResponse> EnterStudentExamScoreAsync(EnterStudentExamScoreRequest request)
        {
            try
            {
                var student = _dbContext.Students.SingleOrDefault(s => s.Id == request.StudentId);
                if (student == null)
                    return new EnterStudentExamScoreResponse { IsSuccess = false, Message = "ERROR: Geçersiz id girdiniz." };

                var studentGrade = student.Grade;
                var currentSemester = CommonFunctions.GetCurrentSemester();
                if (currentSemester == 0)
                    return new EnterStudentExamScoreResponse { IsSuccess = false, Message = "Not girmek için aktif semester bulunmamaktadır.Eylül-Ocak veya Ocak-Haziran ayları arasında not girişi yapılabilir." };

                var examResult = new ExamResult
                {
                    Score = request.ExamScore,
                    LessonId = request.LessonId,
                    StudentId = request.StudentId,
                    Grade = studentGrade,
                    Semester = currentSemester
                };

                await _dbContext.AddAsync(examResult);
                await _dbContext.SaveChangesAsync();

                return new EnterStudentExamScoreResponse { IsSuccess = true, Message = $"{request.StudentId} Id bilgisine sahip öğrenci için not girişi başarılı!" };
            }
            catch (DbUpdateException dbex)
            {
                return new EnterStudentExamScoreResponse { IsSuccess=false,Message = "Veritabanına kayıt sırasında bir sorun oluştu." };
            }
            catch (Exception ex)
            {
                //Log to db or file etc. ex.Message
                return new EnterStudentExamScoreResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }
    }
}
