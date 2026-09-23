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
    public class CommentsController : BaseController
    {
        private readonly ICommentsService _service;

        public CommentsController(ICommentsService service, ILogger<BaseController> logger)
            : base(logger)
        {
            _service = service;
        }

        /// <summary>Get all comments.</summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return HandleResponse(result, "Comments retrieved successfully.");
        }

        /// <summary>Get all comments for a specific work item.</summary>
        [HttpGet("workitem/{workItemId:int}")]
        public async Task<IActionResult> GetByWorkItem(int workItemId)
        {
            var result = await _service.GetByWorkItemAsync(workItemId);
            return HandleResponse(result, "Comments for work item retrieved successfully.");
        }

        /// <summary>Get a single comment by Id.</summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return HandleResponse(result, "Comment retrieved successfully.");
        }

        /// <summary>Create a new comment on a work item.</summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CommentsDTO model)
        {
            if (!ModelState.IsValid)
                return HandleValidationError(ModelState);

            var result = await _service.InsertAsync(model);
            return result.Success
                ? Ok(new ApiResponse<int>(true, "Comment created successfully.", result.Entity))
                : BadRequest(new ApiResponse<object>(false, result.Message ?? "Failed to create comment."));
        }

        /// <summary>Update a comment.</summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] CommentsDTO model)
        {
            if (!ModelState.IsValid)
                return HandleValidationError(ModelState);

            var result = await _service.UpdateAsync(id, model);
            return HandleResponse(result, "Comment updated successfully.");
        }

        /// <summary>Delete a comment.</summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return HandleResponse(result, "Comment deleted successfully.");
        }
    }
}
