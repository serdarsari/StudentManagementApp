
namespace StudentManagement.DTO.TeacherDTO
{
    public class AssignMultipleStudentToTeacherRequest
    {
        public int TeacherId { get; set; }
        public List<int> StudentIds { get; set; }
    }
}
