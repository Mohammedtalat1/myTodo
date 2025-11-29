using Application.Common;
using Application.DTOs;
using Application.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.Application.DTOs;
using TODO.Domain.Entities;

namespace TODO.Application.IService
{
    public interface IAuthService
    {
        Task<Results<AuthResponseDTO>> LoginAsync(LoginDTO dto); // يرجع JWT
        Task<Results<bool>> ChangePasswordAsync(int userId, ChangePasswordDTO dto);
        Task<Results<bool>> LogoutAsync(int userId); // يمكن إدارة RefreshToken هنا إذا أردنا
    }

}
