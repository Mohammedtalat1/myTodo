using TODO.Application.IService;
using TODO.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TODO.Application.Service.Impl
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Users user)  
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
	            new Claim("Id", user.Id.ToString()), 
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
	            new Claim(ClaimTypes.Name, user.Username),
	            new Claim(ClaimTypes.Email, user.Email),
	            new Claim(ClaimTypes.Role, user.RoleId.ToString()),
	            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


			var expires = DateTime.UtcNow.AddMinutes(
				Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])
			);


			var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
