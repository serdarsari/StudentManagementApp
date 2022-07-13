
using MediatR;
using StudentManagement.DTO.BaseClasses;
using StudentManagement.DTO.LessonDTO;

namespace StudentManagement.Service.Core.Features.Queries.GetLessons
{
    public partial class GetLessonsQuery : GetAllBaseRequest, IRequest<GetLessonsResponse>
    {
    }
}
