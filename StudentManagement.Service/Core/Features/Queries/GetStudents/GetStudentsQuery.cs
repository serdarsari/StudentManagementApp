using MediatR;
using StudentManagement.DTO.BaseClasses;
using StudentManagement.DTO.StudentDTO;

namespace StudentManagement.Service.Core.Features.Queries.GetStudents
{
    public partial class GetStudentsQuery : GetAllBaseRequest, IRequest<GetStudentsResponse>
    {
    }
}
