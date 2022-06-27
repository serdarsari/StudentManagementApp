
namespace StudentManagement.DTO.StudentDTO
{
    public class GetStudentsResponse
    {
        public int TotalStudents { get; set; }
        public string NextPage { get; set; }
        public List<StudentResponse> Students { get; set; }
    }
}
