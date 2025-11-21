using Application.Common;
using AutoMapper;
using TODO.Application.DTOs;
using TODO.Application.IService;
using TODO.Domain.Entites;
using TODO.Domain.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TODO.Infrastructure.Service.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService; // خدمة JWT نفترضها

        public AuthService(IAccountRepository accountRepo, IMapper mapper, IJwtService jwtService)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<Results<string>> LoginAsync(LoginDTO dto)
        {
            var account = await _accountRepo.GetByEmailAsync(dto.Email);
            if (account == null || account.Password != dto.Password)
                return ErrorResult.Failed("Invalid credentials");

            var token = _jwtService.GenerateToken(account); // أنشئ JWT
            return SuccessResult.Success(token);
        }

        public async Task<Results<bool>> ChangePasswordAsync(int userId, ChangePasswordDTO dto)
        {
            var account = await _accountRepo.GetByIdAsync(userId);
            if (account == null || account.Password != dto.CurrentPassword)
                return ErrorResult.Failed<bool>("Current password is incorrect");

            account.Password = dto.NewPassword;
            await _accountRepo.UpdateAsync(account);
            return SuccessResult.Success(true);
        }

        public async Task<Results<bool>> LogoutAsync(int userId)
        {
            // إذا نستخدم RefreshToken يمكن حذفه من DB أو Cache
            return SuccessResult.Success(true);
        }
    }

}
