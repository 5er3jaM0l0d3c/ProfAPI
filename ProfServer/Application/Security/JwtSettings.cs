using System;

namespace ProfServer.Application.Security
{
    public sealed class JwtSettings
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Key { get; set; } = null!;
        public int ExpiresMinutes { get; set; } = 60;
    }
}