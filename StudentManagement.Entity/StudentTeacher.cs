using System.ComponentModel.DataAnnotations;


namespace StudentManagement.Entity
{
    public class StudentTeacher
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
