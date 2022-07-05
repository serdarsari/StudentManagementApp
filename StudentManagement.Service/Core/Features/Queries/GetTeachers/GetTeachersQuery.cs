using MediatR;
using StudentManagement.DTO.BaseClasses;
using StudentManagement.DTO.TeacherDTO;

namespace StudentManagement.Service.Core.Features.Queries.GetTeachers
{
    public partial class GetTeachersQuery : GetAllBaseRequest, IRequest<GetTeachersResponse>
    {
    }
}
