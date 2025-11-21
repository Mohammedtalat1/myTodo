using Application.Common;
using TODO.Application.DTOs;
using TODO.Application.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TODO.API.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController(ILogger<RolesController> logger, IRolesService service) : ControllerBase
    {
        private readonly IRolesService _service = service;
        private readonly ILogger<RolesController> _logger = logger;

        [HttpGet]
        public async Task<ActionResult<Results<IEnumerable<RolesDTO>>>> Get()
        {
            try
            {
                return await _service.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get All Users");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}")));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Results<RolesDTO>>> Get(int id)
        {
            try
            {
                return await _service.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get User By Id");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}")));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RolesDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.InsertAsync(model);
                    return StatusCode(200, SuccessResult.Success(ServiceSuccess.Default));
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Create Role");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}")));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RolesDTO model)
        {
            try
            {
                await _service.UpdateAsync(id, model);
                return StatusCode(200, SuccessResult.Success(ServiceSuccess.Default));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Update Role");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}")));
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return StatusCode(200, SuccessResult.Success(ServiceSuccess.Default));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Delete User");
                return StatusCode(500, ErrorResult.Failed(ServiceError.CustomMessage(
                    $"ExpMsg: {ex.Message}. {(ex.InnerException != null ? "InnerMsg: " + ex.InnerException.Message : "")}")));
            }
        }
    }
}
