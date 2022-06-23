
namespace StudentManagement.DTO.StudentDTO
{
    public class CreateStudentRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmergencyCall { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int Grade { get; set; }
        public string ClassBranch { get; set; }
    }
}
