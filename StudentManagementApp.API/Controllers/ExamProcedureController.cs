using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Service.Core.Features.Commands.EnterStudentExamScore;

namespace StudentManagementApp.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ExamProcedureController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamProcedureController(IMediator mediator)
        {
            _mediator = mediator;
        }

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
