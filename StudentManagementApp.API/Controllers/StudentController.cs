using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTO.StudentDTO;
using StudentManagement.Service.StudentService;

namespace StudentManagementApp.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents(GetStudentsRequest request)
        {
            var result = await _service.GetStudentsAsync(request);
            return Ok(result);
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudentDetail(int studentId)
        {
            var result = await _service.GetStudentDetailAsync(studentId);
            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentRequest request)
        {
            var result = await _service.CreateStudentAsync(request);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            var result = await _service.DeleteStudentAsync(studentId);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut("{studentId}")]
        public async Task<IActionResult> UpdateStudent(UpdateStudentRequest request)
        {
            var result = await _service.UpdateStudentAsync(request);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
