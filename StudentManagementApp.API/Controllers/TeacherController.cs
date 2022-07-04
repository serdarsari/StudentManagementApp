using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Service.Core.Features.Commands.AssignMultipleStudentToTeacher;
using StudentManagement.Service.Core.Features.Commands.CreateTeacher;
using StudentManagement.Service.Core.Features.Commands.DeleteTeacher;
using StudentManagement.Service.Core.Features.Commands.UpdateTeacher;
using StudentManagement.Service.Core.Features.Queries.GetTeacherDetail;
using StudentManagement.Service.Core.Features.Queries.GetTeachers;

namespace StudentManagementApp.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeacherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachers(GetTeachersQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{teacherId}")]
        public async Task<IActionResult> GetTeacherDetail(int teacherId)
        {
            var query = new GetTeacherDetailQuery { TeacherId = teacherId };
            var result = await _mediator.Send(query);

            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher(CreateTeacherCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("{teacherId}")]
        public async Task<IActionResult> DeleteTeacher(int teacherId)
        {
            var query = new DeleteTeacherCommand { TeacherId = teacherId };
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeacher(UpdateTeacherCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost]
        [Route("AssignMultipleStudentToTeacher")]
        public async Task<IActionResult> AssignMultipleStudentToTeacher(AssignMultipleStudentToTeacherCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        //private readonly ITeacherService _service;

        //public TeacherController(ITeacherService service)
        //{
        //    _service = service;
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetTeachers(GetTeachersRequest request)
        //{
        //    var result = await _service.GetTeachersAsync(request);
        //    return Ok(result);
        //}

        //[HttpGet("{teacherId}")]
        //public async Task<IActionResult> GetTeacherDetail(int teacherId)
        //{
        //    var result = await _service.GetTeacherDetailAsync(teacherId);
        //    if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
        //        return BadRequest(result.ErrorMessage);

        //    return Ok(result);
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateTeacher(CreateTeacherRequest request)
        //{
        //    var result = await _service.CreateTeacherAsync(request);

        //    if (!result.IsSuccess)
        //        return BadRequest(result.Message);

        //    return Ok(result.Message);
        //}

        //[HttpDelete("{teacherId}")]
        //public async Task<IActionResult> DeleteTeacher(int teacherId)
        //{
        //    var result = await _service.DeleteTeacherAsync(teacherId);

        //    if (!result.IsSuccess)
        //        return BadRequest(result.Message);

        //    return Ok(result.Message);
        //}

        //[HttpPut("{teacherId}")]
        //public async Task<IActionResult> UpdateTeacher(UpdateTeacherRequest request)
        //{
        //    var result = await _service.UpdateTeacherAsync(request);

        //    if (!result.IsSuccess)
        //        return BadRequest(result.Message);

        //    return Ok(result.Message);
        //}

        //[HttpPost]
        //[Route("AssignMultipleStudentToTeacher")]
        //public async Task<IActionResult> AssignMultipleStudentToTeacher(AssignMultipleStudentToTeacherRequest request)
        //{
        //    var result = await _service.AssignMultipleStudentToTeacherAsync(request);
        //    if (!result.IsSuccess)
        //        return BadRequest(result.Message);

        //    return Ok(result.Message);
        //}
    }
}
