using MediatR;
using StudentManagement.DTO.TeacherDTO;

namespace StudentManagement.Service.Core.Features.Queries.GetTeacherDetail
{
    public partial class GetTeacherDetailQuery : IRequest<GetTeacherDetailResponse>
    {
        public int TeacherId { get; set; }
    }
}
