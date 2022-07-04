using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.ParentDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.ParentService
{
    public class ParentService : IParentService
    {
        private readonly StudentManagementAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        public ParentService(StudentManagementAppDbContext dbContext, IMapper mapper, ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<CreateParentResponse> CreateParentAsync(CreateParentRequest request)
        {
            try
            {
                var parent = _mapper.Map<Parent>(request);
                await _dbContext.Parents.AddAsync(parent);
                await _dbContext.SaveChangesAsync();

                return new CreateParentResponse { IsSuccess = true,Message="Oluşturma işlemi başarılı!" };
            }
            catch(DbUpdateException dbex)
            {
                _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                return new CreateParentResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
            }
            catch (Exception ex)
            {
                _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                return new CreateParentResponse { IsSuccess = false, Message= "Bilinmeyen bir hata oluştu." };
            }
        }

        public async Task<AssignSingleStudentToParentResponse> AssignSingleStudentToParentAsync(AssignSingleStudentToParentRequest request)
        {
            try
            {
                var parent = await _dbContext.Parents.SingleOrDefaultAsync(p => p.Id == request.ParentId);
                if (parent == null)
                {
                    _loggerService.Log("AssignSingleStudentToParentAsync invalid teacherId attempt.", CustomLogLevel.Warning);
                    return new AssignSingleStudentToParentResponse { IsSuccess = false, Message = "ERROR: Geçersiz 'TeacherId' bilgisi girdiniz." };
                }

                var parentStudent = new ParentStudent
                {
                    ParentId = request.ParentId,
                    StudentId = request.StudentId
                };

                await _dbContext.ParentStudent.AddAsync(parentStudent);
                await _dbContext.SaveChangesAsync();

                return new AssignSingleStudentToParentResponse { IsSuccess = true, Message = "Atama işlemi başarılı!" };
            }
            catch(DbUpdateException dbex)
            {
                _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                return new AssignSingleStudentToParentResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
            }
            catch (Exception ex)
            {
                _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                return new AssignSingleStudentToParentResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }
    }
}
