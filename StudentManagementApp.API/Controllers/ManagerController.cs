using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Entity.Enums;
using StudentManagement.Service.Core.Features.Commands.CreateManager;
using StudentManagementApp.API.Authorization;

namespace StudentManagementApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]s")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateManager(CreateManagerCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
