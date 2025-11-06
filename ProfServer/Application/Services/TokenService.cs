using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProfServer.Application.Interfaces;
using ProfServer.Application.Security;

namespace ProfServer.Application.Services
{
    public sealed class TokenService : ITokenService
    {
        private readonly JwtSettings _settings;
        private readonly byte[] _keyBytes;

        public TokenService(IOptions<JwtSettings> options)
        {
            _settings = options.Value ?? throw new ArgumentNullException(nameof(options));
            if (string.IsNullOrWhiteSpace(_settings.Key))
                throw new ArgumentException("JWT key is not configured. Set Jwt:Key in configuration or environment.", nameof(_settings));
            _keyBytes = Encoding.UTF8.GetBytes(_settings.Key);
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(_settings.ExpiresMinutes);

            var signingKey = new SymmetricSecurityKey(_keyBytes);
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}