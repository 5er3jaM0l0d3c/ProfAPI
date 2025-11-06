using System.Collections.Generic;
using System.Security.Claims;

namespace ProfServer.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}