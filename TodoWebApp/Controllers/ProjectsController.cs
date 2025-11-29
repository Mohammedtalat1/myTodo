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
    public class ProjectsController : BaseController
    {
        private readonly IProjectsService _service;

        public ProjectsController(IProjectsService service, ILogger<BaseController> logger)
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
                    return HandleError(new Exception(result.Message), "Failed to retrieve Projects.");

                return HandleResponse(result, "Projects retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while retrieving all Projects.");
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
                    return HandleError(new Exception(result.Message), $"Failed to retrieve Project with Id {id}.");

                return HandleResponse(result, "Project retrieved successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while retrieving Project by Id.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectsDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.InsertAsync(model);
                if (!result.Success)
                    return HandleError(new Exception(result.Message), "Failed to create Project.");

                return HandleResponse(result, "Project created successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while creating Project.");
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProjectsDTO model)
        {
            try
            {
                var result = await _service.UpdateAsync(id, model);
                if (!result.Success)
                    return HandleError(new Exception(result.Message), "Failed to update Project.");

                return HandleResponse(result, "Project updated successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while updating Project.");
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
                    return HandleError(new Exception(result.Message), "Failed to delete Project.");

                return HandleResponse(result, "Project deleted successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while deleting Project.");
            }
        }
    }
}
