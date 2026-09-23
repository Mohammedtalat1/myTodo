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
    public class UsersController : BaseController
    {
        private readonly IUserService _service;

        public UsersController(IUserService service, ILogger<BaseController> logger)
            : base(logger)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return HandleResponse(result, "Users retrieved successfully.");
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return HandleResponse(result, "User retrieved successfully.");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO model)
        {
            if (!ModelState.IsValid)
                return HandleValidationError(ModelState);

            var result = await _service.InsertAsync(model);
            return result.Success
                ? Ok(new ApiResponse<int>(true, "User created successfully.", result.Entity))
                : BadRequest(new ApiResponse<object>(false, result.Message ?? "Failed to create user."));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDTO model)
        {
            if (!ModelState.IsValid)
                return HandleValidationError(ModelState);

            var result = await _service.UpdateAsync(id, model);
            return HandleResponse(result, "User updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return HandleResponse(result, "User deleted successfully.");
        }
    }
}