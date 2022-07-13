

namespace StudentManagement.Entity
{
    public class ExamResult
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int Grade { get; set; }
        public int Semester { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
