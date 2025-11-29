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
    public class WorkItemsController : BaseController
    {
        private readonly IWorkItemsService _service;

        public WorkItemsController(IWorkItemsService service, ILogger<BaseController> logger)
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
                    return HandleError(new Exception(result.Message), "Failed to retrieve WorkItems.");

                return HandleResponse(result, "WorkItems retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while retrieving all WorkItems.");
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
                    return HandleError(new Exception(result.Message), $"Failed to retrieve WorkItem with Id {id}.");

                return HandleResponse(result, "WorkItem retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while retrieving WorkItem by Id.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkItemsDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.InsertAsync(model);
                if (!result.Success)
                    return HandleError(new Exception(result.Message), "Failed to create WorkItem.");

                return HandleResponse(result, "WorkItem created successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while creating WorkItem.");
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] WorkItemsDTO model)
        {
            try
            {
                var result = await _service.UpdateAsync(id, model);
                if (!result.Success)
                    return HandleError(new Exception(result.Message), "Failed to update WorkItem.");

                return HandleResponse(result, "WorkItem updated successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while updating WorkItem.");
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
                    return HandleError(new Exception(result.Message), "Failed to delete WorkItem.");

                return HandleResponse(result, "WorkItem deleted successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while deleting WorkItem.");
            }
        }
    }
}
