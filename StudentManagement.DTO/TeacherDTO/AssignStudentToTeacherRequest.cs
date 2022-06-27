
namespace StudentManagement.DTO.TeacherDTO
{
    public class AssignStudentToTeacherRequest
    {
        public int TeacherId { get; set; }
        public List<int> StudentIds { get; set; }
    }
}
