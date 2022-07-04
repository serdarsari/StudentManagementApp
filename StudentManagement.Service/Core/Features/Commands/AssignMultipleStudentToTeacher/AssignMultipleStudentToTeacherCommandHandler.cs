using MediatR;
using StudentManagement.DTO.TeacherDTO;
using StudentManagement.Service.Core.IConfiguration;

namespace StudentManagement.Service.Core.Features.Commands.AssignMultipleStudentToTeacher
{
    public partial class AssignMultipleStudentToTeacherCommand
    {
        public class AssignMultipleStudentToTeacherCommandHandler : IRequestHandler<AssignMultipleStudentToTeacherCommand, AssignMultipleStudentToTeacherResponse>
        {
            private readonly IUnitOfWork _unitOfWork;

            public AssignMultipleStudentToTeacherCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<AssignMultipleStudentToTeacherResponse> Handle(AssignMultipleStudentToTeacherCommand request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Teachers.AssignMultipleStudentToTeacherAsync(request);
            }
        }
    }
}
