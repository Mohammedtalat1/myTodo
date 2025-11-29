using Application.Common;
using Application.DTOs;
using AutoMapper;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TODO.Application.DTOs;
using TODO.Application.IService;
using TODO.Domain.Entities;
using TODO.Domain.IRepository;
using Microsoft.Extensions.Configuration;

namespace TODO.Application.Service.Impl
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepo;
		private readonly IMapper _mapper;
		private readonly IConfiguration _config;
		private readonly IJwtService _jwtService;
		private readonly ILogger<AuthService> _logger;

		public AuthService(
			IUserRepository userRepo,
			IMapper mapper,
			IJwtService jwtService,
			IConfiguration config,
			ILogger<AuthService> logger)
		{
			_userRepo = userRepo;
			_mapper = mapper;
			_jwtService = jwtService;
			_logger = logger;
			_config = config;
		}

		public async Task<Results<AuthResponseDTO>> LoginAsync(LoginDTO dto)
		{
			try
			{
				var user = await _userRepo.GetByEmailAsync(dto.Email);
				if (user == null)
				{
					_logger.LogWarning("Failed login attempt for email {Email}", dto.Email);
					return ErrorResult.Failed<AuthResponseDTO>("Invalid credentials");
				}

				bool isPasswordValid = false;
				try
				{
					isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
				}
				catch
				{
					_logger.LogWarning("Password format invalid for email {Email}", dto.Email);
				}

				if (!isPasswordValid)
				{
					_logger.LogWarning("Failed login attempt for email {Email}", dto.Email);
					return ErrorResult.Failed<AuthResponseDTO>("Invalid credentials");
				}

				var token = _jwtService.GenerateToken(user);
				_logger.LogInformation("User {Email} logged in successfully", dto.Email);

				var minutes = _config.GetValue<int>("Jwt:DurationInMinutes");

				var entity = new AuthResponseDTO
				{
					Token = token,
					Expiration = $"{minutes} minutes"
				};

				return SuccessResult.Success(entity);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred during login for {Email}", dto.Email);
				return ErrorResult.Failed<AuthResponseDTO>("An error occurred during login");
			}
		}

		public async Task<Results<bool>> ChangePasswordAsync(int userId, ChangePasswordDTO dto)
		{
			try
			{
				var user = await _userRepo.GetByIdAsync(userId);
				if (user == null || !BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.Password))
				{
					_logger.LogWarning("Failed password change attempt for userId {UserId}", userId);
					return ErrorResult.Failed<bool>("Current password is incorrect");
				}

				user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
				await _userRepo.UpdateAsync(user);

				_logger.LogInformation("Password changed successfully for userId {UserId}", userId);
				return SuccessResult.Success(true);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while changing password for userId {UserId}", userId);
				return ErrorResult.Failed<bool>("An error occurred while changing password");
			}
		}

		public async Task<Results<bool>> LogoutAsync(int userId)
		{
			try
			{
				_logger.LogInformation("User with Id {UserId} logged out", userId);
				return SuccessResult.Success(true);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred during logout for userId {UserId}", userId);
				return ErrorResult.Failed<bool>("An error occurred during logout");
			}
		}
	}
}
