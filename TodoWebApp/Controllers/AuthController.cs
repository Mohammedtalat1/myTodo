using TODO.Application.DTOs;
using TODO.Application.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using ReservePro.Management.Api.Controllers; // BaseController namespace

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

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                var result = await _authService.LoginAsync(dto);

                if (!result.Success)
                    return HandleError(new Exception(result.Message), "Login failed.");

                return HandleResponse(result, "Login successful.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while logging in.");
            }
        }

        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            try
            {
                int userId = int.Parse(User.Claims.First(c => c.Type == "Id").Value);
                var result = await _authService.ChangePasswordAsync(userId, dto);

                if (!result.Success)
                    return HandleError(new Exception(result.Message), "Failed to change password.");

                return HandleResponse(result, "Password changed successfully.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while changing password.");
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                int userId = int.Parse(User.Claims.First(c => c.Type == "Id").Value);
                var result = await _authService.LogoutAsync(userId);

                if (!result.Success)
                    return HandleError(new Exception(result.Message), "Failed to logout.");

                return HandleResponse(result, "Logout successful.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error occurred while logging out.");
            }
        }
    }
}
