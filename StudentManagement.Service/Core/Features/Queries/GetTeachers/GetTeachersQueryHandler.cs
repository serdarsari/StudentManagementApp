using AutoMapper;
using MediatR;
using StudentManagement.DTO.TeacherDTO;
using StudentManagement.Service.Core.IConfiguration;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core.Features.Queries.GetTeachers
{
    public partial class GetTeachersQuery
    {
        public class GetTeachersQueryHandler : IRequestHandler<GetTeachersQuery, GetTeachersResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ILoggerService _loggerService;

            public GetTeachersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService loggerService)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _loggerService = loggerService;
            }
            public async Task<GetTeachersResponse> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
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
                        Profession = t.Profession
                    }).ToList();

                response.Teachers = responseTeachers;
                return response;
            }
        }
    }
}
