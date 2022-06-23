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

        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] CreateTeacherRequest request)
        {
            var result = await _service.CreateTeacherAsync(request);

            if (!result.IsSuccess)
                return BadRequest(result.Message);


            return Ok(result.Message);
        }
    }
}
