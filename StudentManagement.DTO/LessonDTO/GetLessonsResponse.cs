
using StudentManagement.DTO.BaseClasses;

namespace StudentManagement.DTO.LessonDTO
{
    public class GetLessonsResponse : BaseResponse
    {
        public int TotalLessons { get; set; }
        public string NextPage { get; set; }
        public List<LessonResponse> Lessons { get; set; }
    }
}
