using MediatR;
using StudentManagement.DTO.StudentDTO;

namespace StudentManagement.Service.Core.Features.Queries.GetStudentDetail
{
    public partial class GetStudentDetailQuery : IRequest<GetStudentDetailResponse>
    {
        public int StudentId { get; set; }
    }
}
