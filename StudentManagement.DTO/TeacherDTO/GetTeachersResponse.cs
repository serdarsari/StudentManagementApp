
using StudentManagement.DTO.BaseClasses;

namespace StudentManagement.DTO.TeacherDTO
{
    public class GetTeachersResponse : BaseResponse
    {
        public int TotalTeachers { get; set; }
        public string NextPage { get; set; }
        public List<TeacherResponse> Teachers { get; set; }
    }
}
