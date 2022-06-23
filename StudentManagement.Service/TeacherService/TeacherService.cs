using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.TeacherDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Service.TeacherService
{
    public class TeacherService : ITeacherService
    {
        private readonly StudentManagementAppDbContext _dbContext;

        public TeacherService(StudentManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<GetTeacherDetailResponse> GetTeacherDetailAsync(int teacherId)
        {
            try
            {

                var teacher = await _dbContext.Teachers.SingleOrDefaultAsync(t => t.Id == teacherId);
                if (teacher == null)
                    return new GetTeacherDetailResponse { Message = "ERROR: Geçersiz id girdiniz." };

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
                    Message = "Success",
                };

                return response;

            }
            catch (Exception ex)
            {
                return new GetTeacherDetailResponse { Message = ex.Message };
            }
        }
        public async Task<CreateTeacherResponse> CreateTeacherAsync(CreateTeacherRequest request)
        {
            try
            {
                var lastId = 0;
                if (_dbContext.Teachers.FirstOrDefault() != null)
                {
                    lastId = await _dbContext.Teachers.MaxAsync(t => t.Id);
                }

                string newRegistrationNumber = $"{RegistrationNumberTypes.TCHR.ToString() + (lastId + 1)}";

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

                return new CreateTeacherResponse { IsSuccess = true ,Message = "Öğretmen oluşturma işlemi başarılı!"};

            }
            catch (Exception ex)
            {
                return new CreateTeacherResponse { IsSuccess= false, Message = ex.Message };
            }
        }

        public async Task<DeleteTeacherResponse> DeleteTeacherAsync(int teacherId)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateTeacherResponse> UpdateTeacherAsync(int teacherId, UpdateTeacherRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
