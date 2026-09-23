using Application.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace TODO.API.Controllers
{
    /// <summary>
    /// Base controller providing shared HTTP response helpers.
    /// SRP: all HTTP response shaping lives here — controllers only call these helpers.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> _logger;

        protected BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Returns 200 OK with a structured body, or 404 if result is null.
        /// </summary>
        protected IActionResult HandleResponse<T>(Results<T> result, string successMessage)
        {
            if (!result.Success)
            {
                _logger.LogWarning("Operation failed: {Message}", result.Message);
                return BadRequest(new ApiResponse<T>(false, result.Message ?? "Operation failed."));
            }

            if (result.Entity == null)
            {
                _logger.LogWarning("Resource not found: {Message}", successMessage);
                return NotFound(new ApiResponse<T>(false, "Resource not found."));
            }

            return Ok(new ApiResponse<T>(true, successMessage, result.Entity));
        }

        /// <summary>
        /// Returns 200 OK for success results without a data payload.
        /// </summary>
        protected IActionResult HandleResponse(Results result, string successMessage)
        {
            if (!result.Success)
            {
                _logger.LogWarning("Operation failed: {Message}", result.Message);
                return BadRequest(new ApiResponse<object>(false, result.Message ?? "Operation failed."));
            }

            return Ok(new ApiResponse<object>(true, successMessage));
        }

        /// <summary>
        /// Returns 400 BadRequest with model validation errors.
        /// </summary>
        protected IActionResult HandleValidationError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            _logger.LogWarning("Validation failed: {Errors}", string.Join("; ", errors));
            return BadRequest(new { Success = false, Message = "Validation failed.", Errors = errors });
        }

        /// <summary>
        /// Returns 500 with a structured error body. Use only for unexpected exceptions.
        /// </summary>
        protected IActionResult HandleError(Exception ex, string message = "An unexpected error occurred.")
        {
            _logger.LogError(ex, message);
            return StatusCode(500, new ApiResponse<object>(false, message));
        }
    }

    /// <summary>
    /// Uniform API response envelope — all endpoints return this shape.
    /// </summary>
    public sealed record ApiResponse<T>(bool Success, string Message, T? Data = default);
}
