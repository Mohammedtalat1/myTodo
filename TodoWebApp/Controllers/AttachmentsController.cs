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
    public class AttachmentsController : BaseController
    {
        private readonly IAttachmentsService _service;

        public AttachmentsController(IAttachmentsService service, ILogger<BaseController> logger)
            : base(logger)
        {
            _service = service;
        }

        /// <summary>Get all attachments.</summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return HandleResponse(result, "Attachments retrieved successfully.");
        }

        /// <summary>Get all attachments for a specific work item.</summary>
        [HttpGet("workitem/{workItemId:int}")]
        public async Task<IActionResult> GetByWorkItem(int workItemId)
        {
            var result = await _service.GetByWorkItemAsync(workItemId);
            return HandleResponse(result, "Attachments for work item retrieved successfully.");
        }

        /// <summary>Get a single attachment by Id.</summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return HandleResponse(result, "Attachment retrieved successfully.");
        }

        /// <summary>Add an attachment metadata record to a work item.</summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AttachmentsDTO model)
        {
            if (!ModelState.IsValid)
                return HandleValidationError(ModelState);

            var result = await _service.InsertAsync(model);
            return result.Success
                ? Ok(new ApiResponse<int>(true, "Attachment created successfully.", result.Entity))
                : BadRequest(new ApiResponse<object>(false, result.Message ?? "Failed to create attachment."));
        }

        /// <summary>Update attachment metadata.</summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] AttachmentsDTO model)
        {
            if (!ModelState.IsValid)
                return HandleValidationError(ModelState);

            var result = await _service.UpdateAsync(id, model);
            return HandleResponse(result, "Attachment updated successfully.");
        }

        /// <summary>Delete an attachment.</summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return HandleResponse(result, "Attachment deleted successfully.");
        }
    }
}
