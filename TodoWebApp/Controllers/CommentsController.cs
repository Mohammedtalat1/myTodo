using TODO.Application.DTOs;
using TODO.Application.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ReservePro.Management.Api.Controllers;
using TODO.Application.Entities;

namespace TODO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : BaseController
    {
        private readonly ICommentsService _service;

        public CommentsController(ICommentsService service, ILogger<BaseController> logger)
            : base(logger)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                if (!result.Success)
                    return HandleError(new Exception(result.Message), "Failed to retrieve Comments.");

                return HandleResponse(result, "Comments retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while retrieving all Comments.");
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (!result.Success)
                    return HandleError(new Exception(result.Message), $"Failed to retrieve Comment with Id {id}.");

                return HandleResponse(result, "Comment retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while retrieving Comment by Id.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CommentsDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.InsertAsync(model);
                if (!result.Success)
                    return HandleError(new Exception(result.Message), "Failed to create Comment.");

                return HandleResponse(result, "Comment created successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while creating Comment.");
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CommentsDTO model)
        {
            try
            {
                var result = await _service.UpdateAsync(id, model);
                if (!result.Success)
                    return HandleError(new Exception(result.Message), "Failed to update Comment.");

                return HandleResponse(result, "Comment updated successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while updating Comment.");
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                if (!result.Success)
                    return HandleError(new Exception(result.Message), "Failed to delete Comment.");

                return HandleResponse(result, "Comment deleted successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while deleting Comment.");
            }
        }
    }
}
