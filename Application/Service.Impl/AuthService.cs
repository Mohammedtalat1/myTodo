using Application.Common;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TODO.Application.DTOs;
using TODO.Application.IService;
using TODO.Domain.Entities;
using TODO.Domain.IRepository;

namespace TODO.Application.Service.Impl
{
    /// <summary>
    /// Handles authentication: register, login, change password, logout.
    /// SRP: only auth concerns live here. User CRUD is in UserService.
    /// DIP: depends on interfaces IUserRepository, IRolesRepository, IJwtService.
    /// OCP: each auth operation is a separate method — adding refresh tokens is an extension, not a modification.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRolesRepository _rolesRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IUserRepository userRepo,
            IRolesRepository rolesRepo,
            IMapper mapper,
            IJwtService jwtService,
            IConfiguration config,
            ILogger<AuthService> logger)
        {
            _userRepo = userRepo;
            _rolesRepo = rolesRepo;
            _mapper = mapper;
            _jwtService = jwtService;
            _config = config;
            _logger = logger;
        }

        /// <summary>
        /// Register a new user. Validates uniqueness, hashes password, auto-assigns default role.
        /// </summary>
        public async Task<Results<AuthResponseDTO>> RegisterAsync(RegisterDTO dto)
        {
            try
            {
                if (!await IsEmailUniqueAsync(dto.Email))
                    return ErrorResult.Failed<AuthResponseDTO>("Email is already in use.");

                if (!await IsUsernameUniqueAsync(dto.Username))
                    return ErrorResult.Failed<AuthResponseDTO>("Username is already taken.");

                var defaultRole = await _rolesRepo.GetByNameAsync("Developer");
                if (defaultRole == null)
                    return ErrorResult.Failed<AuthResponseDTO>("Default role not found. Please seed the Roles table.");

                var user = BuildUser(dto, defaultRole.Id);
                await _userRepo.InsertAsync(user);

                _logger.LogInformation("New user registered: {Email}", dto.Email);
                return BuildAuthResponse(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration for {Email}", dto.Email);
                return ErrorResult.Failed<AuthResponseDTO>("An error occurred during registration.");
            }
        }

        /// <summary>Authenticate user by email + password. Returns a JWT on success.</summary>
        public async Task<Results<AuthResponseDTO>> LoginAsync(LoginDTO dto)
        {
            try
            {
                var user = await _userRepo.GetByEmailAsync(dto.Email);
                if (user == null || !VerifyPassword(dto.Password, user.Password))
                {
                    _logger.LogWarning("Failed login attempt for {Email}", dto.Email);
                    return ErrorResult.Failed<AuthResponseDTO>("Invalid credentials.");
                }

                _logger.LogInformation("User {Email} logged in successfully", dto.Email);
                return BuildAuthResponse(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for {Email}", dto.Email);
                return ErrorResult.Failed<AuthResponseDTO>("An error occurred during login.");
            }
        }

        public async Task<Results<bool>> ChangePasswordAsync(int userId, ChangePasswordDTO dto)
        {
            try
            {
                var user = await _userRepo.GetByIdAsync(userId);
                if (user == null || !VerifyPassword(dto.CurrentPassword, user.Password))
                {
                    _logger.LogWarning("Failed password change for userId {UserId}", userId);
                    return ErrorResult.Failed<bool>("Current password is incorrect.");
                }

                user.Password = HashPassword(dto.NewPassword);
                user.UpdatedAt = DateTime.UtcNow;
                await _userRepo.UpdateAsync(user);

                _logger.LogInformation("Password changed for userId {UserId}", userId);
                return SuccessResult.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password for userId {UserId}", userId);
                return ErrorResult.Failed<bool>("An error occurred while changing password.");
            }
        }

        public async Task<Results<bool>> LogoutAsync(int userId)
        {
            _logger.LogInformation("User {UserId} logged out", userId);
            return SuccessResult.Success(true);
        }

        // ── Private helpers ────────────────────────────────────────────────────────

        private async Task<bool> IsEmailUniqueAsync(string email)
            => await _userRepo.GetByEmailAsync(email) == null;

        private async Task<bool> IsUsernameUniqueAsync(string username)
            => await _userRepo.GetByUsernameAsync(username) == null;

        private static bool VerifyPassword(string plain, string hashed)
        {
            try { return BCrypt.Net.BCrypt.Verify(plain, hashed); }
            catch { return false; }
        }

        private static string HashPassword(string plain)
            => BCrypt.Net.BCrypt.HashPassword(plain);

        private static Users BuildUser(RegisterDTO dto, int roleId)
            => new()
            {
                FullName = dto.FullName,
                Username = dto.Username,
                Email = dto.Email,
                Password = HashPassword(dto.Password),
                RoleId = roleId,
                CreatedAt = DateTime.UtcNow
            };

        private Results<AuthResponseDTO> BuildAuthResponse(Users user)
        {
            var token = _jwtService.GenerateToken(user);
            var minutes = _config.GetValue<int>("Jwt:DurationInMinutes");
            return SuccessResult.Success(new AuthResponseDTO
            {
                Token = token,
                Expiration = $"{minutes} minutes"
            });
        }
    }
}
