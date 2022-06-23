
namespace StudentManagement.DTO.ExamProcedureDTO
{
    public class EnterStudentExamScoreRequest
    {
        public int LessonId { get; set; }
        public int StudentId { get; set; }
        public int ExamScore { get; set; }
    }
}
