
using MediatR;
using StudentManagement.DTO.LessonDTO;
using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Features.Queries.GetLessons
{
    public partial class GetLessonsQuery
    {
        public class GetLessonsQueryHandler : IRequestHandler<GetLessonsQuery, GetLessonsResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILoggerService _loggerService;

            public GetLessonsQueryHandler(IUnitOfWork unitOfWork, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _loggerService = loggerService;
            }
            public async Task<GetLessonsResponse> Handle(GetLessonsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var currentStartRow = (request.PageNumber - 1) * request.PageSize;
                    var response = new GetLessonsResponse
                    {
                        NextPage = $"api/Lessons?PageNumber={request.PageNumber + 1}&PageSize={request.PageSize}",
                        TotalLessons = await _unitOfWork.Lessons.CountAsync(),
                    };

                    var lessons = await _unitOfWork.Lessons.GetAllAsync();

                    var responseLessons = lessons.Skip(currentStartRow).Take(request.PageSize)
                        .Select(t => new LessonResponse
                        {
                            Id = t.Id,
                            LessonCode = t.LessonCode,
                            Name = t.Name,
                            Description = t.Description
                        }).ToList();

                    response.Lessons = responseLessons;
                    return response;
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                    return new GetLessonsResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
                }
                
            }
        }
    }
}
