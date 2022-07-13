using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Entity
{
    public class LessonStudent
    {
        [Key]
        public int Id { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
