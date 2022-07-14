using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.ParentDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.Features.Commands.AssignSingleStudentToParent;
using StudentManagement.Service.Core.Features.Queries.GetParents;
using StudentManagement.Service.Core.Features.Queries.GetParentsByTeacher;
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

        public async Task<GetParentsResponse> GetParentsWithChild(GetParentsQuery request)
        {
            try
            {
                var currentStartRow = (request.PageNumber - 1) * request.PageSize;
                var response = new GetParentsResponse
                {
                    NextPage = $"api/Parents?PageNumber={request.PageNumber + 1}&PageSize={request.PageSize}",
                    TotalParents = await _dbContext.Parents.CountAsync(),
                };

                var parents = _dbContext.Parents
                    .Join(_dbContext.ParentStudent, sc => sc.Id, soc => soc.ParentId, (sc, soc) => new { ParentStudent = soc, Parent = sc })
                    .Join(_dbContext.Students, sc2 => sc2.ParentStudent.StudentId, soc2 => soc2.Id, (sc2, soc2) => new { Student = soc2, A = sc2 })
                    .Select(s => new ParentResponse
                    {
                        Id = s.A.Parent.Id,
                        FirstName = s.A.Parent.FirstName,
                        LastName = s.A.Parent.LastName,
                        PhoneNumber = s.A.Parent.PhoneNumber,
                        ChildFullName = s.Student.FirstName + " " + s.Student.LastName,
                        Gender = s.A.Parent.Gender
                        
                    }).ToList();

                response.Parents = parents;
                return response;
            }
            catch (Exception ex)
            {
                _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                return new GetParentsResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }

        public async Task<GetParentsByTeacherResponse> GetParentsByTeacher(GetParentsByTeacherQuery request)
        {
            try
            {
                var currentStartRow = (request.PageNumber - 1) * request.PageSize;
                var response = new GetParentsByTeacherResponse
                {
                    NextPage = $"api/Parents?PageNumber={request.PageNumber + 1}&PageSize={request.PageSize}",
                    TotalParents = await _dbContext.Parents.CountAsync(),
                };

                var parents = await _dbContext.StudentTeacher.Where(x => x.TeacherId == request.TeacherId)
                    .Join(_dbContext.Students, sc => sc.StudentId, soc => soc.Id, (sc, soc) => new { sc, soc })
                    .Join(_dbContext.ParentStudent, sc2 => sc2.soc.Id, soc2 => soc2.StudentId, (sc2, soc2) => new { sc2, soc2 })
                    .Join(_dbContext.Parents, sc3 => sc3.soc2.ParentId, soc3 => soc3.Id, (sc3, soc3) => new { sc3, soc3 })
                    .Select(x => new ParentResponse
                    {
                        Id = x.soc3.Id,
                        FirstName = x.soc3.FirstName,
                        LastName = x.soc3.LastName,
                        PhoneNumber = x.soc3.PhoneNumber,
                        ChildFullName = x.sc3.sc2.soc.FirstName + " " + x.sc3.sc2.soc.LastName,
                        Gender = x.soc3.Gender
                    }).ToListAsync();

                response.Parents = parents;
                return response;
            }
            catch (Exception ex)
            {
                _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                return new GetParentsByTeacherResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }
    }
}
