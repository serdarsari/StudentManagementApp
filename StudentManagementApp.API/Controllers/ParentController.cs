using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTO.ParentDTO;
using StudentManagement.Service.ParentService;

namespace StudentManagementApp.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _service;

        public ParentController(IParentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateParent([FromBody] CreateParentRequest request)
        {
            var result = await _service.CreateParentAsync(request);
            if(!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost]
        [Route("AssignSingleStudentToParent")]
        public async Task<IActionResult> AssignSingleStudentToParentAsync([FromBody] AssignSingleStudentToParentRequest request)
        {
            var result = await _service.AssignSingleStudentToParentAsync(request);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
