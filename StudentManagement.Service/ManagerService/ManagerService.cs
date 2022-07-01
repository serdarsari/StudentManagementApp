using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.ManagerDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Common;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LogService;

namespace StudentManagement.Service.ManagerService
{
    public class ManagerService : IManagerService
    {
        private readonly StudentManagementAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        public ManagerService(StudentManagementAppDbContext dbContext, IMapper mapper, ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _loggerService = loggerService;
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
                _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                return new CreateManagerResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
            }
            catch (Exception ex)
            {
                _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                return new CreateManagerResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
            }

        }
    }
}
