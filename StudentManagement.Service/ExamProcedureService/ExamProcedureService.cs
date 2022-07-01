using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.ExamProcedureDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Common;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LogService;

namespace StudentManagement.Service.ExamProcedureService
{
    public class ExamProcedureService : IExamProcedureService
    {
        private readonly StudentManagementAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        public ExamProcedureService(StudentManagementAppDbContext dbContext, IMapper mapper, ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<EnterStudentExamScoreResponse> EnterStudentExamScoreAsync(EnterStudentExamScoreRequest request)
        {
            try
            {
                var student = await _dbContext.Students.SingleOrDefaultAsync(s => s.Id == request.StudentId);
                if (student == null)
                {
                    _loggerService.Log("EnterStudentExamScoreAsync invalid StudentId attempt.", CustomLogLevel.Warning);
                    return new EnterStudentExamScoreResponse { IsSuccess = false, Message = "ERROR: Geçersiz 'StudentId' bilgisi girdiniz." };
                }

                var studentGrade = student.Grade;
                var currentSemester = CommonFunctions.GetCurrentSemester();
                if (currentSemester == 0)
                    return new EnterStudentExamScoreResponse { IsSuccess = false, Message = "Not girmek için aktif semester bulunmamaktadır. Eylül-Ocak veya Ocak-Haziran ayları arasında not girişi yapılabilir." };

                var examResult = _mapper.Map<ExamResult>(request);
                examResult.Grade = studentGrade;
                examResult.Semester = currentSemester;
                
                await _dbContext.ExamResults.AddAsync(examResult);
                await _dbContext.SaveChangesAsync();

                return new EnterStudentExamScoreResponse { IsSuccess = true, Message = $"{request.StudentId} Id bilgisine sahip öğrenci için not girişi başarılı!" };
            }
            catch (DbUpdateException dbex)
            {
                _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                return new EnterStudentExamScoreResponse { IsSuccess=false,Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
            }
            catch (Exception ex)
            {
                _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                return new EnterStudentExamScoreResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }
    }
}
