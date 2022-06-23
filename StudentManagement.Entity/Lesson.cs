using System.ComponentModel.DataAnnotations;


namespace StudentManagement.Entity
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        public string LessonCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Teacher> Instructors { get; set; }

    }
}
