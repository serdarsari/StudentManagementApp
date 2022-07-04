using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.StudentDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Common;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly StudentManagementAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        public StudentService(StudentManagementAppDbContext dbContext, IMapper mapper, ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<GetStudentsResponse> GetStudentsAsync(GetStudentsRequest request)
        {
            var currentStartRow = (request.PageNumber - 1) * request.PageSize;
            var response = new GetStudentsResponse
            {
                NextPage = $"api/Students?PageNumber={request.PageNumber + 1}&PageSize={request.PageSize}",
                TotalStudents = await _dbContext.Students.CountAsync(),
            };

            var students = await _dbContext.Students.ToListAsync();

            var responseStudents = students.Skip(currentStartRow).Take(request.PageSize)
                .Select(s => new StudentResponse
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Grade = s.Grade,
                    ClassBranch = s.ClassBranch
                }).ToList();

            response.Students = responseStudents;
            return response;
        }

        public async Task<GetStudentDetailResponse> GetStudentDetailAsync(int studentId)
        {
            try
            {
                var student = await _dbContext.Students.SingleOrDefaultAsync(s => s.Id == studentId);
                if (student == null)
                {
                    _loggerService.Log("GetStudentDetailAsync invalid studentId attempt.", CustomLogLevel.Warning);
                    return new GetStudentDetailResponse { ErrorMessage = "ERROR: Geçersiz 'studentId' bilgisi girdiniz." };
                }
                
                var response = _mapper.Map<GetStudentDetailResponse>(student);

                return response;
            }
            catch (Exception ex)
            {
                _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                return new GetStudentDetailResponse { ErrorMessage = "Bilinmeyen bir hata oluştu." };
            }
        }

        public async Task<CreateStudentResponse> CreateStudentAsync(CreateStudentRequest request)
        {
            try
            {
                var lastId = 0;
                var checkIfDatabaseIsEmpty = _dbContext.Students.FirstOrDefault();
                if (checkIfDatabaseIsEmpty != null)
                    lastId = await _dbContext.Students.MaxAsync(t => t.Id);

                var newId = lastId + 1;
                string newRegistrationNumber = RegistrationNumberGenerator.Create(RegistrationNumberTypes.STDN, newId);

                var student = _mapper.Map<Student>(request);
                student.StudentId = newRegistrationNumber;

                await _dbContext.Students.AddAsync(student);
                await _dbContext.SaveChangesAsync();

                return new CreateStudentResponse { IsSuccess = true , Message = "Oluşturma işlemi başarılı!"};
            }
            catch (DbUpdateException dbex)
            {
                _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                return new CreateStudentResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
            }
            catch (Exception ex)
            {
                _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                return new CreateStudentResponse { IsSuccess=false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }

        public async Task<DeleteStudentResponse> DeleteStudentAsync(int studentId)
        {
            try
            {
                var student = await _dbContext.Students.SingleOrDefaultAsync(s => s.Id == studentId);
                if (student == null)
                {
                    _loggerService.Log("DeleteStudentAsync invalid studentId attempt.", CustomLogLevel.Warning);
                    return new DeleteStudentResponse { IsSuccess = false, Message = "ERROR: Geçersiz 'studentId' bilgisi girdiniz." };
                }

                _dbContext.Students.Remove(student);
                await _dbContext.SaveChangesAsync();

                return new DeleteStudentResponse { IsSuccess = true, Message = "Silme işlemi başarılı!" };
            }
            catch(DbUpdateException dbex)
            {
                _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                return new DeleteStudentResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
            }
            catch (Exception ex)
            {
                _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                return new DeleteStudentResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }

        public async Task<UpdateStudentResponse> UpdateStudentAsync(UpdateStudentRequest request)
        {
            try
            {
                var student = await _dbContext.Students.SingleOrDefaultAsync(s => s.Id == request.StudentId);
                if (student == null)
                {
                    _loggerService.Log("UpdateStudentAsync invalid studentId attempt.", CustomLogLevel.Warning);
                    return new UpdateStudentResponse { IsSuccess = false, Message = "ERROR: Geçersiz 'StudentId' bilgisi girdiniz." };
                }

                student.EmergencyCall = request.EmergencyCall != student.EmergencyCall ? request.EmergencyCall : student.EmergencyCall;
                student.Address = request.Address != student.Address ? request.Address : student.Address;
                student.ClassBranch = request.ClassBranch != student.ClassBranch ? request.ClassBranch : student.ClassBranch;
                student.GPA = request.GPA != student.GPA ? request.GPA : student.GPA;

                await _dbContext.SaveChangesAsync();

                return new UpdateStudentResponse { IsSuccess = true, Message = "Güncelleme işlemi başarılı!" };
            }
            catch(DbUpdateException dbex)
            {
                _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                return new UpdateStudentResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
            }
            catch (Exception ex)
            {
                _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                return new UpdateStudentResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }
    }
}
