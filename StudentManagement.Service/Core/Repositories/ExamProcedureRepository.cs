using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.ExamProcedureDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Common;
using StudentManagement.Service.Core.Features.Commands.EnterStudentExamScore;
using StudentManagement.Service.Core.Features.Queries.GetExamResultsByStudent;
using StudentManagement.Service.Core.IRepositories;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Repositories
{
	public class ExamProcedureRepository : GenericRepository<ExamResult>, IExamProcedureRepository
	{
		private readonly IMapper _mapper;

		public ExamProcedureRepository(StudentManagementAppDbContext dbContext, ILoggerService loggerService, IMapper mapper) : base(dbContext, loggerService)
		{
			_mapper = mapper;
		}

		public async Task<EnterStudentExamScoreResponse> EnterStudentExamScoreAsync(EnterStudentExamScoreCommand request)
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

				var checkExistScoreWithSameSemester = await _dbContext.ExamResults.FirstOrDefaultAsync(s => s.LessonId == request.LessonId && s.StudentId == student.Id && s.Semester == currentSemester && s.Grade == studentGrade && s.Semester == currentSemester);
				if (checkExistScoreWithSameSemester != null)
					return new EnterStudentExamScoreResponse { IsSuccess = false, Message = "Bu öğrenciye, bu dersin, bu semester notu zaten girilmiş!" };
				//if (currentSemester == 0)
				//    return new EnterStudentExamScoreResponse { IsSuccess = false, Message = "Not girmek için aktif semester bulunmamaktadır. Eylül-Ocak veya Ocak-Haziran ayları arasında not girişi yapılabilir." };

				var examResult = _mapper.Map<ExamResult>(request);
				examResult.Grade = studentGrade;
				examResult.Semester = currentSemester;

				var examCount = await _dbContext.ExamResults.Where(e => e.StudentId == student.Id).CountAsync();
				var totalScore = (examCount * student.GPA) + request.Score;
				var newGPA = totalScore / (examCount + 1);
				student.GPA = Math.Round(newGPA, 2);

				await _dbContext.ExamResults.AddAsync(examResult);
				await _dbContext.SaveChangesAsync();

				return new EnterStudentExamScoreResponse { IsSuccess = true, Message = $"{request.StudentId} Id bilgisine sahip öğrenci için not girişi başarılı!" };
			}
			catch (DbUpdateException dbex)
			{
				_loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
				return new EnterStudentExamScoreResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
			}
			catch (Exception ex)
			{
				_loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
				return new EnterStudentExamScoreResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
			}
		}

		public async Task<GetExamResultsByStudentResponse> GetExamResultsByStudent(GetExamResultsByStudentQuery request)
		{
			try
			{
				var student = await _dbContext.Students.SingleOrDefaultAsync(x => x.Id == request.StudentId);

				if (student == null)
					return new GetExamResultsByStudentResponse { IsSuccess = false, Message = "StudentId bilgisi hatalı!" };
				var gpa = student.GPA;

				var result = await _dbContext.ExamResults.Where(x => x.StudentId == request.StudentId)
					.Join(_dbContext.Students, sc => sc.StudentId, soc => soc.Id, (sc, soc) => new { sc, soc })
					.Join(_dbContext.Lessons, sc2 => sc2.sc.LessonId, soc2 => soc2.Id, (sc2, soc2) => new { sc2, soc2 })
					.Select(x => new ExamResultsByStudent
					{
						LessonCode = x.soc2.LessonCode,
						LessonName = x.soc2.Name,
						Score = x.sc2.sc.Score,
						
					}).ToListAsync();
				
				var response = new GetExamResultsByStudentResponse
				{
					ExamResults = result,
					GPA = gpa
				};

				return response;
			}
			catch (Exception ex)
			{
				_loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
				return new GetExamResultsByStudentResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
			}
		}
	}
}
