using MediatR;
using StudentManagement.DTO.StudentDTO;
using StudentManagement.Service.Core.IConfigurationRepository;

namespace StudentManagement.Service.Core.Features.Queries.GetstudentsByTeacher
{
    public partial class GetStudentsByTeacherQuery
    {
        public class GetStudentsByTeacherQueryHandler : IRequestHandler<GetStudentsByTeacherQuery, GetStudentsByTeacherResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            public GetStudentsByTeacherQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<GetStudentsByTeacherResponse> Handle(GetStudentsByTeacherQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Students.GetStudentsByTeacher(request);
            }
        }
    }
}
