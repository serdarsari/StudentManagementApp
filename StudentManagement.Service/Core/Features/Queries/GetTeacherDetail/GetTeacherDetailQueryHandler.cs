using AutoMapper;
using MediatR;
using StudentManagement.DTO.TeacherDTO;
using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Features.Queries.GetTeacherDetail
{
    public partial class GetTeacherDetailQuery
    {
        public class GetTeacherDetailQueryHandler : IRequestHandler<GetTeacherDetailQuery, GetTeacherDetailResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ILoggerService _loggerService;

            public GetTeacherDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _loggerService = loggerService;
            }

            public async Task<GetTeacherDetailResponse> Handle(GetTeacherDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var teacher = await _unitOfWork.Teachers.GetByIdAsync(request.TeacherId);
                    if (teacher == null)
                    {
                        _loggerService.Log("GetTeacherDetailAsync invalid teacherId attempt.", CustomLogLevel.Warning);
                        return new GetTeacherDetailResponse { ErrorMessage = "ERROR: Geçersiz 'teacherId' bilgisi girdiniz." };
                    }

                    var response = _mapper.Map<GetTeacherDetailResponse>(teacher);
                    return response;
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                    return new GetTeacherDetailResponse { ErrorMessage = "Bilinmeyen bir hata oluştu." };
                }
            }
        }
    }
}
