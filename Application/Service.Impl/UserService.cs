using Application.Common;
using Application.Service.Impl.BaseService;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TODO.Application.IService;
using TODO.Domain.Entities;
using TODO.Application.Entities;
using TODO.Domain.IRepository;

public class UserService : BaseService<Users, UserDTO>, IUserService
{
    public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        : base(userRepository, mapper, logger)
    {
    }

    public override async Task<Results<int>> InsertAsync(UserDTO value)
    {
        try
        {
            // Hash the password before saving
            value.Password = BCrypt.Net.BCrypt.HashPassword(value.Password);

            return await base.InsertAsync(value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inserting user");
            return ErrorResult.Failed<int>(ex.Message);
        }
    }
    public override async Task<Results<int>> UpdateAsync(int id, UserDTO value)
    {
        try
        {
            if (!string.IsNullOrEmpty(value.Password))
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
