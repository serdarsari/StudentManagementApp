using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.ParentDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.Features.Commands.AssignSingleStudentToParent;
using StudentManagement.Service.Core.IRepositories;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Repositories
{
    public class ParentRepository : GenericRepository<Parent>, IParentRepository
    {
        public ParentRepository(StudentManagementAppDbContext dbContext, ILoggerService loggerService) : base(dbContext, loggerService)
        {
        }

        public async Task<AssignSingleStudentToParentResponse> AssignSingleStudentToParentAsync(AssignSingleStudentToParentCommand request)
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
            catch (DbUpdateException dbex)
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
