using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Entity.Enums;
using StudentManagement.Service.Core.Features.Commands.CreateStudent;
using StudentManagement.Service.Core.Features.Commands.DeleteStudent;
using StudentManagement.Service.Core.Features.Commands.UpdateStudent;
using StudentManagement.Service.Core.Features.Queries.GetStudentDetail;
using StudentManagement.Service.Core.Features.Queries.GetStudents;
using StudentManagement.Service.Core.Features.Queries.GetStudentsByLesson;
using StudentManagement.Service.Core.Features.Queries.GetstudentsByTeacher;
using StudentManagementApp.API.Authorization;

namespace StudentManagementApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]s")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetStudents([FromQuery] GetStudentsQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [Authorize(Role.Teacher, Role.Admin)]
        [HttpGet]
        [Route("GetStudentsByTeacher")]
        public async Task<IActionResult> GetStudentsByTeacher([FromQuery] GetStudentsByTeacherQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [Authorize(Role.Teacher, Role.Admin)]
        [HttpGet]
        [Route("GetStudentsByLesson")]
        public async Task<IActionResult> GetStudentsByLesson([FromQuery] GetStudentsByLessonQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [Authorize(Role.Teacher, Role.Admin)]
        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudentDetail(int studentId)
        {
            var query = new GetStudentDetailQuery { StudentId = studentId };
            var result = await _mediator.Send(query);

            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [Authorize(Role.Admin)]
        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            var query = new DeleteStudentCommand { StudentId = studentId };
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [Authorize(Role.Admin)]
        [HttpPut]
        public async Task<IActionResult> UpdateStudent(UpdateStudentCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
