using MediatR;
using StudentManagement.DTO.BaseClasses;
using StudentManagement.DTO.LessonDTO;

namespace StudentManagement.Service.Core.Features.Queries.GetLessonsByTeacher
{
    public partial class GetLessonsByTeacherQuery : GetAllBaseRequest, IRequest<GetLessonsByTeacherResponse>
    {
        public int TeacherId { get; set; }
    }
}
