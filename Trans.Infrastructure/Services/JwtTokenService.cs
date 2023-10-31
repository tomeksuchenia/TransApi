using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Trans.Infrastructure.Dto;
using Trans.Infrastructure.Settings;

namespace Trans.Infrastructure.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _settings;
        public JwtTokenService(JwtSettings settings)
        {
            _settings = settings;
        }
        public JwtDto Create(Guid id, string role)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, id.ToString()),
                new Claim(ClaimTypes.Role, role),
            };

            var expires = DateTime.UtcNow.AddDays(_settings.ExpiryDay);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super_key_123456789!")),
                SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(

                issuer: _settings.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
                );
                
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                Token = token,
                ExpiresTime = expires
            };

        }
    }
}
