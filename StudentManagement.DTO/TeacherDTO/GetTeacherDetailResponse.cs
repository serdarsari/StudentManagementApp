
namespace StudentManagement.DTO.TeacherDTO
{
    public class GetTeacherDetailResponse
    {
        public string RegistrationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public int Age
        {
            get
            {
                return DateTime.Now.Year - Birthday.Year;
            }
        }
        public string Profession { get; set; }
        public string? Description { get; set; }
        public string Email { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
