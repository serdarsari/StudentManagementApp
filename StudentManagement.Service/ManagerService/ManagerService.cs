using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.ManagerDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Common;
using StudentManagement.Service.Enums;

namespace StudentManagement.Service.ManagerService
{
    public class ManagerService : IManagerService
    {
        private readonly StudentManagementAppDbContext _dbContext;

        public ManagerService(StudentManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateManagerResponse> CreateManagerAsync(CreateManagerRequest request)
        {
            try
            {
                var lastId = 0;
                var checkIfDatabaseIsEmpty = _dbContext.Managers.FirstOrDefault();
                if (checkIfDatabaseIsEmpty != null)
                    lastId = await _dbContext.Managers.MaxAsync(t => t.Id);

                var newId = lastId + 1;
                string newRegistrationNumber = RegistrationNumberGenerator.Create(RegistrationNumberTypes.MNGR, newId);

                var manager = new Manager
                {
                    RegistrationNumber = newRegistrationNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                };

                await _dbContext.AddAsync(manager);
                await _dbContext.SaveChangesAsync();

                return new CreateManagerResponse { IsSuccess = true, Message = "Oluşturma işlemi başarılı!" };
            }
            catch (DbUpdateException dbex)
            {
                return new CreateManagerResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu." };
            }
            catch (Exception ex)
            {
                return new CreateManagerResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }

        }
    }
}
