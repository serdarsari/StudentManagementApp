using System.ComponentModel.DataAnnotations;


namespace StudentManagement.Entity
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public int LessonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Profession { get; set; }
        public string? Description { get; set; }
        public string Email { get; set; }

    }
}
