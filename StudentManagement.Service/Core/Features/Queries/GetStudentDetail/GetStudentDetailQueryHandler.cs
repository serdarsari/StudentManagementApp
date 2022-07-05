using AutoMapper;
using MediatR;
using StudentManagement.DTO.StudentDTO;
using StudentManagement.Service.Core.IConfiguration;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Features.Queries.GetStudentDetail
{
    public partial class GetStudentDetailQuery
    {
        public class GetStudentDetailQueryHandler : IRequestHandler<GetStudentDetailQuery, GetStudentDetailResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ILoggerService _loggerService;

            public GetStudentDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _loggerService = loggerService;
            }
            public async Task<GetStudentDetailResponse> Handle(GetStudentDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var student = await _unitOfWork.Students.GetByIdAsync(request.StudentId);
                    if (student == null)
                    {
                        _loggerService.Log("GetStudentDetailAsync invalid studentId attempt.", CustomLogLevel.Warning);
                        return new GetStudentDetailResponse { ErrorMessage = "ERROR: Geçersiz 'studentId' bilgisi girdiniz." };
                    }

                    var response = _mapper.Map<GetStudentDetailResponse>(student);

                    return response;
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                    return new GetStudentDetailResponse { ErrorMessage = "Bilinmeyen bir hata oluştu." };
                }
            }
        }
    }
}
