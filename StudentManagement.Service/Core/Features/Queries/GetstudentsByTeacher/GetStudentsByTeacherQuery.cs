using MediatR;
using StudentManagement.DTO.BaseClasses;
using StudentManagement.DTO.StudentDTO;

namespace StudentManagement.Service.Core.Features.Queries.GetstudentsByTeacher
{
    public partial class GetStudentsByTeacherQuery : GetAllBaseRequest, IRequest<GetStudentsByTeacherResponse>
    {
        public int TeacherId { get; set; }
    }
}
