
namespace StudentManagement.DTO.StudentDTO
{
    public class GetStudentDetailResponse
    {
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmergencyCall { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int Age {
            get
            {
                return DateTime.Now.Year - Birthday.Year;
            }
        }
        public int Grade { get; set; }
        public string ClassBranch { get; set; }
        public double GPA { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
