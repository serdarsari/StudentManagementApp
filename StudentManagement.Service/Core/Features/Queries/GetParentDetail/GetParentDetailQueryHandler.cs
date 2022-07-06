
using AutoMapper;
using MediatR;
using StudentManagement.DTO.ParentDTO;
using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Features.Queries.GetParentDetail
{
    public partial class GetParentDetailQuery
    {
        public class GetParentDetailQueryHandler : IRequestHandler<GetParentDetailQuery, GetParentDetailResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ILoggerService _loggerService;

            public GetParentDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _loggerService = loggerService;
            }
            public async Task<GetParentDetailResponse> Handle(GetParentDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var parent = await _unitOfWork.Parents.GetByIdAsync(request.ParentId);
                    if (parent == null)
                    {
                        _loggerService.Log("GetParentDetailAsync invalid ParentId attempt.", CustomLogLevel.Warning);
                        return new GetParentDetailResponse { ErrorMessage = "ERROR: Geçersiz 'ParentId' bilgisi girdiniz." };
                    }

                    var response = _mapper.Map<GetParentDetailResponse>(parent);

                    return response;
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                    return new GetParentDetailResponse { ErrorMessage = "Bilinmeyen bir hata oluştu." };
                }
            }
        }
    }
}
