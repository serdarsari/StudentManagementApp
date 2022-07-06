using MediatR;
using StudentManagement.DTO.StudentDTO;
using StudentManagement.Service.Core.IConfigurationRepository;

namespace StudentManagement.Service.Core.Features.Queries.GetStudents
{
    public partial class GetStudentsQuery
    {
        public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, GetStudentsResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            public GetStudentsQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<GetStudentsResponse> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
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
                        ClassBranch = s.ClassBranch
                    }).ToList();

                response.Students = responseStudents;
                return response;
            }
        }
    }
}
