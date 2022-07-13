using MediatR;
using StudentManagement.DTO.StudentDTO;
using StudentManagement.Service.Core.IConfigurationRepository;

namespace StudentManagement.Service.Core.Features.Queries.GetStudentsByLesson
{
    public partial class GetStudentsByLessonQuery
    {
        public class GetStudentsByLessonQueryHandler : IRequestHandler<GetStudentsByLessonQuery, GetStudentsByLessonResponse>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetStudentsByLessonQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<GetStudentsByLessonResponse> Handle(GetStudentsByLessonQuery request, CancellationToken cancellationToken)
            {
               return _unitOfWork.LessonStudent.GetStudentsByLesson(request);
            }
        }
    }
}
