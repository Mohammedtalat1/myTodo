using Application.Common;
using Application.Service.Impl.BaseService;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TODO.Application.DTOs;
using TODO.Application.IService;
using TODO.Domain.Entities;
using TODO.Domain.IRepository;

namespace TODO.Application.Service.Impl
{
    /// <summary>
    /// Handles user CRUD operations. Extends BaseService with password hashing.
    /// SRP: only user-specific logic lives here — generic CRUD is in BaseService.
    /// OCP: hashing behavior is added by overriding, not modifying, the base InsertAsync/UpdateAsync.
    /// </summary>
    public class UserService : BaseService<Users, UserDTO>, IUserService
    {
        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<UserService> logger)
            : base(userRepository, mapper, logger)
        {
        }

        /// <summary>
        /// Hashes the password before delegating insert to BaseService.
        /// </summary>
        public override async Task<Results<int>> InsertAsync(UserDTO value)
        {
            try
            {
                value.Password = BCrypt.Net.BCrypt.HashPassword(value.Password);
                return await base.InsertAsync(value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting user");
                return ErrorResult.Failed<int>(ex.Message);
            }
        }

        /// <summary>
        /// Re-hashes password on update only if a new password was provided.
        /// </summary>
        public override async Task<Results<int>> UpdateAsync(int id, UserDTO value)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(value.Password))
                {
                    value.Password = BCrypt.Net.BCrypt.HashPassword(value.Password);
                }

                return await base.UpdateAsync(id, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with Id {Id}", id);
                return ErrorResult.Failed<int>(ex.Message);
            }
        }
    }
}
