
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
        public string Gender { get; set; }     //Bunu belki enum'da tutabilirsin. Tekrar düşün.
        public int Age
        {
            get
            {
                return DateTime.Now.Year - Birthday.Year;
            }
        }
        public string Profession { get; set; }
        public string? Description { get; set; }
        public string? Message { get; set; }
    }
}
