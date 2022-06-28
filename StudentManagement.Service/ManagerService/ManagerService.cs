using AutoMapper;
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
        private readonly IMapper _mapper;

        public ManagerService(StudentManagementAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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

                var manager = _mapper.Map<Manager>(request);
                manager.RegistrationNumber = newRegistrationNumber;

                await _dbContext.Managers.AddAsync(manager);
                await _dbContext.SaveChangesAsync();

                return new CreateManagerResponse { IsSuccess = true, Message = "Oluşturma işlemi başarılı!" };
            }
            catch (DbUpdateException dbex)
            {
                return new CreateManagerResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
            }
            catch (Exception ex)
            {
                return new CreateManagerResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }

        }
    }
}
