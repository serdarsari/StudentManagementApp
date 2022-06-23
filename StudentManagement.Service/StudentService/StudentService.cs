using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.StudentDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Common;
using StudentManagement.Service.Enums;

namespace StudentManagement.Service.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly StudentManagementAppDbContext _dbContext;

        public StudentService(StudentManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
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

                var student = new Student
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    EmergencyCall = request.EmergencyCall,
                    Birthday = request.Birthday,
                    Gender = request.Gender,
                    Address = request.Address,
                    Grade = request.Grade,
                    ClassBranch = request.ClassBranch,
                    Age = DateTime.Now.Year - request.Birthday.Year,
                    StudentId = newRegistrationNumber
                };

                await _dbContext.AddAsync(student);
                await _dbContext.SaveChangesAsync();

                return new CreateStudentResponse { IsSuccess = true , Message = "Öğrenci oluşturma işlemi başarılı!"};
            }
            catch (Exception ex)
            {
                return new CreateStudentResponse { IsSuccess=false, Message = ex.Message };
            }
        }
    }
}
