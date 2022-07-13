using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Entity.Enums;
using StudentManagement.Service.Core.Features.Commands.AssignSingleStudentToParent;
using StudentManagement.Service.Core.Features.Commands.CreateParent;
using StudentManagement.Service.Core.Features.Queries.GetParentDetail;
using StudentManagement.Service.Core.Features.Queries.GetParents;
using StudentManagement.Service.Core.Features.Queries.GetParentsByTeacher;
using StudentManagementApp.API.Authorization;

namespace StudentManagementApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]s")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ParentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetParents([FromQuery] GetParentsQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [Authorize(Role.Teacher, Role.Admin)]
        [HttpGet]
        [Route("GetParentsByTeacher")]
        public async Task<IActionResult> GetParentsByTeacher([FromQuery] GetParentsByTeacherQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [Authorize(Role.Teacher, Role.Admin)]
        [HttpGet("{parentId}")]
        public async Task<IActionResult> GetParentDetail(int parentId)
        {
            var query = new GetParentDetailQuery { ParentId = parentId };
            var result = await _mediator.Send(query);

            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateParent(CreateParentCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        [Route("AssignSingleStudentToParent")]
        public async Task<IActionResult> AssignSingleStudentToParentAsync(AssignSingleStudentToParentCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
