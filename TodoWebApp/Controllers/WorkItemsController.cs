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
    public class WorkItemsController : BaseController
    {
        private readonly IWorkItemsService _service;

        public WorkItemsController(IWorkItemsService service, ILogger<BaseController> logger)
            : base(logger)
        {
            _service = service;
        }

        /// <summary>Get all work items.</summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return HandleResponse(result, "WorkItems retrieved successfully.");
        }

        /// <summary>Get all work items for a specific project.</summary>
        [HttpGet("project/{projectId:int}")]
        public async Task<IActionResult> GetByProject(int projectId)
        {
            var result = await _service.GetByProjectAsync(projectId);
            return HandleResponse(result, "WorkItems for project retrieved successfully.");
        }

        /// <summary>Get all work items in a Kanban board column.</summary>
        [HttpGet("column/{columnId:int}")]
        public async Task<IActionResult> GetByColumn(int columnId)
        {
            var result = await _service.GetByColumnAsync(columnId);
            return HandleResponse(result, "WorkItems for column retrieved successfully.");
        }

        /// <summary>Get a single work item by Id.</summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return HandleResponse(result, "WorkItem retrieved successfully.");
        }

        /// <summary>Create a new work item.</summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkItemsDTO model)
        {
            if (!ModelState.IsValid)
                return HandleValidationError(ModelState);

            var result = await _service.InsertAsync(model);
            return result.Success
                ? Ok(new ApiResponse<int>(true, "WorkItem created successfully.", result.Entity))
                : BadRequest(new ApiResponse<object>(false, result.Message ?? "Failed to create work item."));
        }

        /// <summary>Update a work item.</summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] WorkItemsDTO model)
        {
            if (!ModelState.IsValid)
                return HandleValidationError(ModelState);

            var result = await _service.UpdateAsync(id, model);
            return HandleResponse(result, "WorkItem updated successfully.");
        }

        /// <summary>Move a work item to a different Kanban column.</summary>
        [HttpPut("{id:int}/move/{columnId:int}")]
        public async Task<IActionResult> MoveToColumn(int id, int columnId)
        {
            var result = await _service.MoveToColumnAsync(id, columnId);
            return result.Success
                ? Ok(new ApiResponse<object>(true, "WorkItem moved successfully."))
                : BadRequest(new ApiResponse<object>(false, result.Message ?? "Failed to move work item."));
        }

        /// <summary>Delete a work item.</summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return HandleResponse(result, "WorkItem deleted successfully.");
        }
    }
}
