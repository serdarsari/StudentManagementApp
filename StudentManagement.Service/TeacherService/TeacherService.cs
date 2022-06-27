using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.TeacherDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Common;
using StudentManagement.Service.Enums;

namespace StudentManagement.Service.TeacherService
{
    public class TeacherService : ITeacherService
    {
        private readonly StudentManagementAppDbContext _dbContext;

        public TeacherService(StudentManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetTeachersResponse> GetTeachersAsync(GetTeachersRequest request)
        {
            var currentStartRow = (request.PageNumber - 1) * request.PageSize;
            var response = new GetTeachersResponse
            {
                NextPage = $"api/Teachers?PageNumber={request.PageNumber + 1}&PageSize={request.PageSize}",
                TotalTeachers = await _dbContext.Teachers.CountAsync(),
            };

            var teachers = await _dbContext.Teachers.ToListAsync();

            var responseTeachers = teachers.Skip(currentStartRow).Take(request.PageSize)
                .Select(t => new TeacherResponse
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Profession = t.Profession
                }).ToList();

            response.Teachers = responseTeachers;
            return response;
        }

        public async Task<GetTeacherDetailResponse> GetTeacherDetailAsync(int teacherId)
        {
            try
            {
                var teacher = await _dbContext.Teachers.SingleOrDefaultAsync(t => t.Id == teacherId);
                if (teacher == null)
                    return new GetTeacherDetailResponse { ErrorMessage = "ERROR: Geçersiz 'teacherId' bilgisi girdiniz." };

                var response = new GetTeacherDetailResponse
                {
                    RegistrationNumber = teacher.RegistrationNumber,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    PhoneNumber = teacher.PhoneNumber,
                    Address = teacher.Address,
                    Gender = teacher.Gender,
                    Profession = teacher.Profession,
                    Description = teacher.Description,
                    Birthday = teacher.Birthday,
                };

                return response;
            }
            catch (Exception ex)
            {
                return new GetTeacherDetailResponse { ErrorMessage = "Bilinmeyen bir hata oluştu." };
            }
        }
        public async Task<CreateTeacherResponse> CreateTeacherAsync(CreateTeacherRequest request)
        {
            try
            {
                var lastId = 0;
                var checkIfDatabaseIsEmpty = _dbContext.Teachers.FirstOrDefault();
                if (checkIfDatabaseIsEmpty != null)
                    lastId = await _dbContext.Teachers.MaxAsync(t => t.Id);

                var newId = lastId + 1;
                string newRegistrationNumber = RegistrationNumberGenerator.Create(RegistrationNumberTypes.TCHR, newId);

                var teacher = new Teacher
                {
                    RegistrationNumber = newRegistrationNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    Birthday = request.Birthday,
                    Gender = request.Gender,
                    Profession = request.Profession,
                    Description = request.Description,
                    Age = DateTime.Now.Year - request.Birthday.Year
                };

                await _dbContext.AddAsync(teacher);
                await _dbContext.SaveChangesAsync();

                return new CreateTeacherResponse { IsSuccess = true ,Message = "Oluşturma işlemi başarılı!"};
            }
            catch (DbUpdateException dbex)
            {
                return new CreateTeacherResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu." };
            }
            catch (Exception ex)
            {
                return new CreateTeacherResponse { IsSuccess= false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }

        public async Task<DeleteTeacherResponse> DeleteTeacherAsync(int teacherId)
        {
            try
            {
                var teacher = await _dbContext.Teachers.SingleOrDefaultAsync(t => t.Id == teacherId);
                if (teacher == null)
                    return new DeleteTeacherResponse { IsSuccess = false, Message = "ERROR: Geçersiz 'teacherId' bilgisi girdiniz." };

                _dbContext.Teachers.Remove(teacher);
                await _dbContext.SaveChangesAsync();

                return new DeleteTeacherResponse { IsSuccess = true, Message = "Silme işlemi başarılı!" };
            }
            catch(DbUpdateException dbex)
            {
                return new DeleteTeacherResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu." };
            }
            catch (Exception ex)
            {
                return new DeleteTeacherResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }

        public async Task<UpdateTeacherResponse> UpdateTeacherAsync(int teacherId, UpdateTeacherRequest request)
        {
            try
            {
                var teacher = await _dbContext.Teachers.SingleOrDefaultAsync(t => t.Id == teacherId);
                if (teacher == null)
                    return new UpdateTeacherResponse { IsSuccess = false, Message = "ERROR: Geçersiz 'teacherId' bilgisi girdiniz." };

                teacher.PhoneNumber = request.PhoneNumber != teacher.PhoneNumber ? request.PhoneNumber : teacher.PhoneNumber;
                teacher.Address = request.Address != teacher.Address ? request.Address : teacher.Address;
                teacher.Profession = request.Profession != teacher.Profession ? request.Profession : teacher.Profession;
                teacher.Description = request.Description != teacher.Description ? request.Description : teacher.Description;

                await _dbContext.SaveChangesAsync();

                return new UpdateTeacherResponse { IsSuccess = true, Message = "Güncelleme işlemi başarılı!" };
            }
            catch (DbUpdateException dbex)
            {
                return new UpdateTeacherResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu." };
            }
            catch (Exception ex)
            {
                return new UpdateTeacherResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }
    }
}
