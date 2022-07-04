using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTO.ExamProcedureDTO;
using StudentManagement.Service.ExamProcedureService;

namespace StudentManagementApp.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ExamProcedureController : ControllerBase
    {
        private readonly IExamProcedureService _service;

        public ExamProcedureController(IExamProcedureService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> EnterStudentExamScore(EnterStudentExamScoreRequest request)
        {
            var result = await _service.EnterStudentExamScoreAsync(request);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
