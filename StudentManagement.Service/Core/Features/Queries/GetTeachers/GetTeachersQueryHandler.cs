using MediatR;
using StudentManagement.DTO.TeacherDTO;
using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Features.Queries.GetTeachers
{
    public partial class GetTeachersQuery
    {
        public class GetTeachersQueryHandler : IRequestHandler<GetTeachersQuery, GetTeachersResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILoggerService _loggerService;

            public GetTeachersQueryHandler(IUnitOfWork unitOfWork, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _loggerService = loggerService;
            }
            public async Task<GetTeachersResponse> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var currentStartRow = (request.PageNumber - 1) * request.PageSize;
                    var response = new GetTeachersResponse
                    {
                        NextPage = $"api/Teachers?PageNumber={request.PageNumber + 1}&PageSize={request.PageSize}",
                        TotalTeachers = await _unitOfWork.Teachers.CountAsync(),
                    };

                    var teachers = await _unitOfWork.Teachers.GetAllAsync();

                    var responseTeachers = teachers.Skip(currentStartRow).Take(request.PageSize)
                        .Select(t => new TeacherResponse
                        {
                            Id = t.Id,
                            FirstName = t.FirstName,
                            LastName = t.LastName,
                            Profession = t.Profession,
                            PhoneNumber = t.PhoneNumber,
                            Gender = t.Gender
                        }).ToList();

                    response.Teachers = responseTeachers;
                    return response;
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                    return new GetTeachersResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
                }
            }
        }
    }
}
