using Bll.DTO;
using BLL.DTO;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bll.Services
{
    public class JwtTokenService(IConfiguration configuration)
    {
        public LoginResponceDTO CreateToken(User user)
        {
            var expires = DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:expiresMinutes"]));
            var claims = new List<Claim>
            {
                new("MyClaim", "DATA"),
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Convert.FromBase64String(configuration["Jwt:Key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:issuer"],
                audience: configuration["Jwt:audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credential
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new LoginResponceDTO() { AccessToken = tokenString, ExpiresAtUtc = expires};
        }
    }
}
