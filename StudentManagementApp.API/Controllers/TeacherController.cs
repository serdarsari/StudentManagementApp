using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTO.TeacherDTO;
using StudentManagement.Service.TeacherService;

namespace StudentManagementApp.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _service;

        public TeacherController(ITeacherService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachers([FromQuery] GetTeachersRequest request)
        {
            var result = await _service.GetTeachersAsync(request);
            return Ok(result);
        }

        [HttpGet("{teacherId}")]
        public async Task<IActionResult> GetTeacherDetail(int teacherId)
        {
            var result = await _service.GetTeacherDetailAsync(teacherId);
            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] CreateTeacherRequest request)
        {
            var result = await _service.CreateTeacherAsync(request);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpDelete("{teacherId}")]
        public async Task<IActionResult> DeleteTeacher(int teacherId)
        {
            var result = await _service.DeleteTeacherAsync(teacherId);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut("{teacherId}")]
        public async Task<IActionResult> UpdateTeacher(int teacherId, UpdateTeacherRequest request)
        {
            var result = await _service.UpdateTeacherAsync(teacherId, request);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost]
        [Route("AssignStudentToTeacher")]
        public async Task<IActionResult> AssignStudentToTeacher([FromBody] AssignStudentToTeacherRequest request)
        {
            var result = await _service.AssignStudentToTeacherAsync(request);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
