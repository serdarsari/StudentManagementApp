using StudentManagement.DTO.LessonDTO;

namespace StudentManagement.Service.LessonService
{
    public interface ILessonService
    {
        Task<CreateLessonResponse> CreateLessonAsync(CreateLessonRequest request);
    }
}
