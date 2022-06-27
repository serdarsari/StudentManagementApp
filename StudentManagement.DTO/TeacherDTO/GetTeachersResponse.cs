
namespace StudentManagement.DTO.TeacherDTO
{
    public class GetTeachersResponse
    {
        public int TotalTeachers { get; set; }
        public string NextPage { get; set; }
        public List<TeacherResponse> Teachers { get; set; }
    }
}
