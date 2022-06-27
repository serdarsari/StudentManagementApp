using AutoMapper;
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
        private readonly IMapper _mapper;

        public TeacherService(StudentManagementAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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

                var response = _mapper.Map<GetTeacherDetailResponse>(teacher);
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

                var teacher = _mapper.Map<Teacher>(request);
                teacher.RegistrationNumber = newRegistrationNumber;

                await _dbContext.AddAsync(teacher);
                await _dbContext.SaveChangesAsync();

                return new CreateTeacherResponse { IsSuccess = true ,Message = "Oluşturma işlemi başarılı!"};
            }
            catch (DbUpdateException dbex)
            {
                return new CreateTeacherResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
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
                return new DeleteTeacherResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
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
                return new UpdateTeacherResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
            }
            catch (Exception ex)
            {
                return new UpdateTeacherResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }

        public async Task<AssignStudentToTeacherResponse> AssignStudentToTeacherAsync(AssignStudentToTeacherRequest request)
        {
            try
            {
                var teacher = await _dbContext.Teachers.SingleOrDefaultAsync(t => t.Id == request.TeacherId);
                if (teacher == null)
                    return new AssignStudentToTeacherResponse { IsSuccess = false, Message = "ERROR: Geçersiz 'TeacherId' bilgisi girdiniz." };

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

                await _dbContext.AddRangeAsync(studentTeacherList);
                await _dbContext.SaveChangesAsync();

                return new AssignStudentToTeacherResponse { IsSuccess = true, Message = "Atama işlemi başarılı!" };
            }
            catch (DbUpdateException dbex)
            {
                return new AssignStudentToTeacherResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
            }
            catch (Exception ex)
            {
                return new AssignStudentToTeacherResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }
        }
    }
}
