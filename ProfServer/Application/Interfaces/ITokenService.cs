using ProfServer.Application.DTOs.Responses;
using System.Collections.Generic;
using System.Security.Claims;

namespace ProfServer.Application.Interfaces
{
    public interface ITokenService
    {
        TokenResponse GenerateToken(IEnumerable<Claim> claims);
    }
}