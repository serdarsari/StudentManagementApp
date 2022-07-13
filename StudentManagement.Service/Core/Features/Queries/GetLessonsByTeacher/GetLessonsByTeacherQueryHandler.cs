using MediatR;
using StudentManagement.DTO.LessonDTO;
using StudentManagement.Service.Core.IConfigurationRepository;

namespace StudentManagement.Service.Core.Features.Queries.GetLessonsByTeacher
{
    public partial class GetLessonsByTeacherQuery
    {
        public class GetLessonsByTeacherQueryHandler : IRequestHandler<GetLessonsByTeacherQuery, GetLessonsByTeacherResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            public GetLessonsByTeacherQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<GetLessonsByTeacherResponse> Handle(GetLessonsByTeacherQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Lessons.GetLessonsByTeacher(request);
            }
        }
    }
}
