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
    public class ProjectsController : BaseController
    {
        private readonly IProjectsService _service;

        public ProjectsController(IProjectsService service, ILogger<BaseController> logger)
            : base(logger)
        {
            _service = service;
        }

        /// <summary>Get all projects (admin view).</summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return HandleResponse(result, "Projects retrieved successfully.");
        }

        /// <summary>Get all projects the authenticated user belongs to.</summary>
        [HttpGet("my")]
        public async Task<IActionResult> GetMyProjects()
        {
            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (idClaim == null)
                return Unauthorized(new ApiResponse<object>(false, "Unauthorized."));

            int userId = int.Parse(idClaim.Value);
            var result = await _service.GetUserProjectsAsync(userId);
            return HandleResponse(result, "Your projects retrieved successfully.");
        }

        /// <summary>Get a single project by Id.</summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return HandleResponse(result, "Project retrieved successfully.");
        }

        /// <summary>Create a new project. Creator is auto-added as Owner.</summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectsDTO model)
        {
            if (!ModelState.IsValid)
                return HandleValidationError(ModelState);

            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            int creatorId = idClaim != null ? int.Parse(idClaim.Value) : 0;

            var result = await _service.InsertAsync(model, creatorId);
            return result.Success
                ? Ok(new ApiResponse<int>(true, "Project created successfully.", result.Entity))
                : BadRequest(new ApiResponse<object>(false, result.Message ?? "Failed to create project."));
        }

        /// <summary>Update an existing project.</summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProjectsDTO model)
        {
            if (!ModelState.IsValid)
                return HandleValidationError(ModelState);

            var result = await _service.UpdateAsync(id, model);
            return HandleResponse(result, "Project updated successfully.");
        }

        /// <summary>Delete a project by Id.</summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return HandleResponse(result, "Project deleted successfully.");
        }
    }
}
