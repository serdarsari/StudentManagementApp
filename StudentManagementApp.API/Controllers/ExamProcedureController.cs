using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Entity.Enums;
using StudentManagement.Service.Core.Features.Commands.EnterStudentExamScore;
using StudentManagement.Service.Core.Features.Queries.GetExamResultsByStudent;
using StudentManagementApp.API.Authorization;

namespace StudentManagementApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]s")]
    [ApiController]
    public class ExamProcedureController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamProcedureController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Role.Student,Role.Teacher, Role.Admin)]
        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetExamResultsByStudent(int studentId)
        {
            var query = new GetExamResultsByStudentQuery { StudentId = studentId };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [Authorize(Role.Teacher,Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> EnterStudentExamScore(EnterStudentExamScoreCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
