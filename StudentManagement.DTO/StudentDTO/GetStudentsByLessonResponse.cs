
using StudentManagement.DTO.BaseClasses;

namespace StudentManagement.DTO.StudentDTO
{
    public class GetStudentsByLessonResponse : BaseResponse
    {
        public List<StudentByLessonResponse> Students { get; set; }
    }
}
