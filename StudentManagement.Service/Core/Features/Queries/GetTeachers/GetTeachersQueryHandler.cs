using MediatR;
using StudentManagement.DTO.TeacherDTO;
using StudentManagement.Service.Core.IConfiguration;

namespace StudentManagement.Service.Core.Features.Queries.GetTeachers
{
    public partial class GetTeachersQuery
    {
        public class GetTeachersQueryHandler : IRequestHandler<GetTeachersQuery, GetTeachersResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            public GetTeachersQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
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
