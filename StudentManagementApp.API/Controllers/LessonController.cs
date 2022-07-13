using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Entity.Enums;
using StudentManagement.Service.Core.Features.Commands.CreateLesson;
using StudentManagement.Service.Core.Features.Queries.GetLessons;
using StudentManagement.Service.Core.Features.Queries.GetLessonsByTeacher;
using StudentManagementApp.API.Authorization;

namespace StudentManagementApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]s")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LessonController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Role.Teacher, Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetLessons([FromQuery] GetLessonsQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [Authorize(Role.Teacher, Role.Admin)]
        [HttpGet]
        [Route("GetLessonsByTeacher")]
        public async Task<IActionResult> GetLessonsByTeacher([FromQuery] GetLessonsByTeacherQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateLesson(CreateLessonCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
