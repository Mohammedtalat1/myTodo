using TODO.Application.DTOs;
using TODO.Application.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TODO.API.Controllers;

namespace TODO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectMembersController : BaseController
    {
        private readonly IProjectMemberService _service;

        public ProjectMembersController(IProjectMemberService service, ILogger<BaseController> logger)
            : base(logger)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return HandleResponse(result, "ProjectMembers retrieved successfully.");
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return HandleResponse(result, "ProjectMember retrieved successfully.");
        }

        /// <summary>Add a user to a project. Duplicate memberships are rejected by the service.</summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectMemberDTO model)
        {
            if (!ModelState.IsValid)
                return HandleValidationError(ModelState);

            var result = await _service.InsertAsync(model);
            return result.Success
                ? Ok(new ApiResponse<int>(true, "ProjectMember created successfully.", result.Entity))
                : BadRequest(new ApiResponse<object>(false, result.Message ?? "Failed to create project member."));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProjectMemberDTO model)
        {
            if (!ModelState.IsValid)
                return HandleValidationError(ModelState);

            var result = await _service.UpdateAsync(id, model);
            return HandleResponse(result, "ProjectMember updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return HandleResponse(result, "ProjectMember deleted successfully.");
        }
    }
}
