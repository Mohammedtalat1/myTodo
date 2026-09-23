using Application.Common;
using TODO.Application.DTOs;
using System.Threading.Tasks;

namespace TODO.Application.IService
{
    /// <summary>
    /// Authentication service contract.
    /// ISP: only auth-related operations — no CRUD on Users (that's IUserService).
    /// </summary>
    public interface IAuthService
    {
        Task<Results<AuthResponseDTO>> RegisterAsync(RegisterDTO dto);
        Task<Results<AuthResponseDTO>> LoginAsync(LoginDTO dto);
        Task<Results<bool>> ChangePasswordAsync(int userId, ChangePasswordDTO dto);
        Task<Results<bool>> LogoutAsync(int userId);
    }
}
