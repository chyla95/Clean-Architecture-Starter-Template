using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sample.Authentication.Jwt.Utilities;
internal sealed class JwtClaimsUtility : IJwtClaimsUtility
{
    public Claim? GetClaim(string token, string key)
        => GetClaims(token).SingleOrDefault(c => c.Type == key);

    public Claim? GetClaim(string token, Func<Claim, bool> predicate)
        => GetClaims(token).SingleOrDefault(predicate);

    public IEnumerable<Claim> GetClaims(string token, string key)
        => GetClaims(token).Where(c => c.Type == key);

    public IEnumerable<Claim> GetClaims(string token, Func<Claim, bool> predicate)
        => GetClaims(token).Where(predicate);

    public IEnumerable<Claim> GetClaims(string token)
    {
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        if (!jwtSecurityTokenHandler.CanReadToken(token)) throw new SecurityTokenException("Invalid or unreadable token.");

        JwtSecurityToken jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(token);
        IEnumerable<Claim> claims = jwtSecurityToken.Claims;
        return claims;
    }
}
