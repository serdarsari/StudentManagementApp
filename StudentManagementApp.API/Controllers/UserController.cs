using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Service.Core.Features.Commands.CreateToken;
using StudentManagement.Service.Core.Features.Commands.CreateUser;
using StudentManagement.Service.Core.Features.Commands.RefreshToken;
using StudentManagementApp.API.Authorization;

namespace StudentManagementApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("CreateToken")]
        public async Task<IActionResult> CreateToken(CreateTokenCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromQuery] RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
