using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Entity.Enums;
using StudentManagement.Service.Core.Features.Commands.AssignMultipleStudentToTeacher;
using StudentManagement.Service.Core.Features.Commands.CreateTeacher;
using StudentManagement.Service.Core.Features.Commands.DeleteTeacher;
using StudentManagement.Service.Core.Features.Commands.UpdateTeacher;
using StudentManagement.Service.Core.Features.Queries.GetTeacherDetail;
using StudentManagement.Service.Core.Features.Queries.GetTeachers;
using StudentManagementApp.API.Authorization;

namespace StudentManagementApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]s")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeacherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetTeachers([FromQuery] GetTeachersQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [Authorize(Role.Admin)]
        [HttpGet("{teacherId}")]
        public async Task<IActionResult> GetTeacherDetail(int teacherId)
        {
            var query = new GetTeacherDetailQuery { TeacherId = teacherId };
            var result = await _mediator.Send(query);

            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(CreateTeacherCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [Authorize(Role.Admin)]
        [HttpDelete("{teacherId}")]
        public async Task<IActionResult> DeleteTeacher(int teacherId)
        {
            var query = new DeleteTeacherCommand { TeacherId = teacherId };
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [Authorize(Role.Admin)]
        [HttpPut]
        public async Task<IActionResult> UpdateTeacher(UpdateTeacherCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        [Route("AssignMultipleStudentToTeacher")]
        public async Task<IActionResult> AssignMultipleStudentToTeacher(AssignMultipleStudentToTeacherCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
