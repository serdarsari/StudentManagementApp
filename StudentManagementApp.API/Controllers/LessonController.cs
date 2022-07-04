using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTO.LessonDTO;
using StudentManagement.Service.LessonService;

namespace StudentManagementApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _service;

        public LessonController(ILessonService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLesson(CreateLessonRequest request)
        {
            var result = await _service.CreateLessonAsync(request);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
