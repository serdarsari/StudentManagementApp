using MediatR;
using StudentManagement.DTO.BaseClasses;
using StudentManagement.DTO.StudentDTO;
using StudentManagement.Service.Core.IConfigurationRepository;

namespace StudentManagement.Service.Core.Features.Queries.GetStudentsForrAssignmentToTeacher
{
    public class GetStudentsForAssignmentToTeacherQuery : GetAllBaseRequest, IRequest<GetStudentsForAssignmentToTeacherResponse>
    {
        public int TeacherId { get; set; }
        public class GetStudentsForrAssignmentToTeacherQueryHandler : IRequestHandler<GetStudentsForAssignmentToTeacherQuery, GetStudentsForAssignmentToTeacherResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            public GetStudentsForrAssignmentToTeacherQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<GetStudentsForAssignmentToTeacherResponse> Handle(GetStudentsForAssignmentToTeacherQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Students.GetStudentsForAssignmentToTeacher(request);
            }
        }
    }
}
