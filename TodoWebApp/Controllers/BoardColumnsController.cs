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
    public class BoardColumnsController : BaseController
    {
        private readonly IBoardColumnsService _service;

        public BoardColumnsController(IBoardColumnsService service, ILogger<BaseController> logger)
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
                    return HandleError(new Exception(result.Message), "Failed to retrieve BoardColumns.");

                return HandleResponse(result, "BoardColumns retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while retrieving all BoardColumns.");
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
                    return HandleError(new Exception(result.Message), $"Failed to retrieve BoardColumn with Id {id}.");

                return HandleResponse(result, "BoardColumn retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while retrieving BoardColumn by Id.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BoardColumnsDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.InsertAsync(model);
                if (!result.Success)
                    return HandleError(new Exception(result.Message), "Failed to create BoardColumn.");

                return HandleResponse(result, "BoardColumn created successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while creating BoardColumn.");
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BoardColumnsDTO model)
        {
            try
            {
                var result = await _service.UpdateAsync(id, model);
                if (!result.Success)
                    return HandleError(new Exception(result.Message), "Failed to update BoardColumn.");

                return HandleResponse(result, "BoardColumn updated successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while updating BoardColumn.");
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
                    return HandleError(new Exception(result.Message), "Failed to delete BoardColumn.");

                return HandleResponse(result, "BoardColumn deleted successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while deleting BoardColumn.");
            }
        }
    }
}
