using MediatR;
using StudentManagement.DTO.StudentDTO;
using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.Enums;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Features.Queries.GetStudents
{
    public partial class GetStudentsQuery
    {
        public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, GetStudentsResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILoggerService _loggerService;

            public GetStudentsQueryHandler(IUnitOfWork unitOfWork, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _loggerService = loggerService;
            }

            public async Task<GetStudentsResponse> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var currentStartRow = (request.PageNumber - 1) * request.PageSize;
                    var response = new GetStudentsResponse
                    {
                        NextPage = $"api/Students?PageNumber={request.PageNumber + 1}&PageSize={request.PageSize}",
                        TotalStudents = await _unitOfWork.Students.CountAsync(),
                    };

                    var students = await _unitOfWork.Students.GetAllAsync();

                    var responseStudents = students.Skip(currentStartRow).Take(request.PageSize)
                        .Select(s => new StudentResponse
                        {
                            Id = s.Id,
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            Grade = s.Grade,
                            ClassBranch = s.ClassBranch,
                            GPA = s.GPA,
                            Gender = s.Gender
                        }).ToList();

                    response.Students = responseStudents;
                    return response;
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex.Message, CustomLogLevel.Error, ex.StackTrace);
                    return new GetStudentsResponse { IsSuccess = false, Message = "Bilinmeyen bir hata oluştu." };
                }
            }
        }
    }
}
