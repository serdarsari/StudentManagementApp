using System.ComponentModel.DataAnnotations;


namespace StudentManagement.Entity
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmergencyCall { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }
        public string ClassBranch { get; set; }
        public double GPA { get; set; }
		public string Email { get; set; }
	}
}
