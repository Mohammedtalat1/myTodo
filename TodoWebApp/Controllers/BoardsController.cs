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
    public class BoardsController : BaseController
    {
        private readonly IBoardsService _service;

        public BoardsController(IBoardsService service, ILogger<BaseController> logger)
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
                    return HandleError(new Exception(result.Message), "Failed to retrieve Boards.");

                return HandleResponse(result, "Boards retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while retrieving all Boards.");
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
                    return HandleError(new Exception(result.Message), $"Failed to retrieve Board with Id {id}.");

                return HandleResponse(result, "Board retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while retrieving Board by Id.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BoardsDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.InsertAsync(model);
                if (!result.Success)
                    return HandleError(new Exception(result.Message), "Failed to create Board.");

                return HandleResponse(result, "Board created successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while creating Board.");
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BoardsDTO model)
        {
            try
            {
                var result = await _service.UpdateAsync(id, model);
                if (!result.Success)
                    return HandleError(new Exception(result.Message), "Failed to update Board.");

                return HandleResponse(result, "Board updated successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while updating Board.");
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
                    return HandleError(new Exception(result.Message), "Failed to delete Board.");

                return HandleResponse(result, "Board deleted successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while deleting Board.");
            }
        }
    }
}
