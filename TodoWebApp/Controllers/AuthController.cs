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
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, ILogger<BaseController> logger)
            : base(logger)
        {
            _authService = authService;
        }

        /// <summary>Register a new user account and receive a JWT.</summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return HandleValidationError(ModelState);

            var result = await _authService.RegisterAsync(dto);
            return result.Success
                ? Ok(new ApiResponse<AuthResponseDTO>(true, "Registration successful.", result.Entity))
                : BadRequest(new ApiResponse<AuthResponseDTO>(false, result.Message ?? "Registration failed."));
        }

        /// <summary>Authenticate with email and password. Returns a JWT on success.</summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return HandleValidationError(ModelState);

            var result = await _authService.LoginAsync(dto);
            return result.Success
                ? Ok(new ApiResponse<AuthResponseDTO>(true, "Login successful.", result.Entity))
                : Unauthorized(new ApiResponse<AuthResponseDTO>(false, result.Message ?? "Invalid credentials."));
        }

        /// <summary>Change the authenticated user's password.</summary>
        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return HandleValidationError(ModelState);

            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (idClaim == null)
                return Unauthorized(new ApiResponse<object>(false, "Unauthorized."));

            int userId = int.Parse(idClaim.Value);
            var result = await _authService.ChangePasswordAsync(userId, dto);
            return result.Success
                ? Ok(new ApiResponse<object>(true, "Password changed successfully."))
                : BadRequest(new ApiResponse<object>(false, result.Message ?? "Failed to change password."));
        }

        /// <summary>Logout the current user (client should discard the JWT).</summary>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            int userId = int.Parse(User.Claims.First(c => c.Type == "Id").Value);
            var result = await _authService.LogoutAsync(userId);
            return result.Success
                ? Ok(new ApiResponse<object>(true, "Logout successful."))
                : BadRequest(new ApiResponse<object>(false, result.Message ?? "Failed to logout."));
        }
    }
}
