using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DTO.TeacherDTO;
using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Features.Commands.UpdateTeacher
{
    public partial class UpdateTeacherCommand
    {
        public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, UpdateTeacherResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ILoggerService _loggerService;

            public UpdateTeacherCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _loggerService = loggerService;
            }

            public async Task<UpdateTeacherResponse> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var teacher = await _unitOfWork.Teachers.GetByIdAsync(request.TeacherId);
                    if (teacher == null)
                        return new UpdateTeacherResponse { IsSuccess = false, Message = "ERROR: Geçersiz 'TeacherId' bilgisi girdiniz." };

                    var user = await _unitOfWork.Users.FirstOrDefaultAsync(x => x.Email == teacher.Email);

                    teacher.PhoneNumber = request.PhoneNumber != teacher.PhoneNumber ? request.PhoneNumber : teacher.PhoneNumber;
                    teacher.Address = request.Address != teacher.Address ? request.Address : teacher.Address;
                    teacher.Profession = request.Profession != teacher.Profession ? request.Profession : teacher.Profession;
                    teacher.Description = request.Description != teacher.Description ? request.Description : teacher.Description;
                    teacher.Email = request.Email != teacher.Email ? request.Email : teacher.Email;

                    user.Email = teacher.Email;

                    _unitOfWork.Teachers.Update(teacher);
                    _unitOfWork.Users.Update(user);
                    await _unitOfWork.CompleteAsync();

                    return new UpdateTeacherResponse { IsSuccess = true, Message = "Güncelleme işlemi başarılı!" };
                }
                catch (DbUpdateException dbex)
                {
                    _loggerService.Log(dbex.Message, CustomLogLevel.Error, dbex.StackTrace);
                    return new UpdateTeacherResponse { IsSuccess = false, Message = "Veritabanına kayıt sırasında bir sorun oluştu. İşlem yapmaya çalıştığınız Id'leri kontrol edin." };
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                    return new UpdateTeacherResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
                }
            }
        }
    }
}
