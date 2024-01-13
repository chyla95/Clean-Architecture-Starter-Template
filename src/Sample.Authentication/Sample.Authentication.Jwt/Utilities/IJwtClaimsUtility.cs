using System.Security.Claims;

namespace Sample.Authentication.Jwt.Utilities;
public interface IJwtClaimsUtility
{
    /// <summary>
    /// Retrieves a single claim based on a provided token and key if available.
    /// </summary>
    Claim? GetClaim(string token, string key);

    /// <summary>
    /// Retrieves a single claim based on a token and a condition defined by the provided predicate function.
    /// </summary>
    Claim? GetClaim(string token, Func<Claim, bool> predicate);

    /// <summary>
    /// Retrieves multiple claims that match the given token and key. Returns an enumerable collection of claims.
    /// </summary>
    IEnumerable<Claim> GetClaims(string token, string key);

    /// <summary>
    /// Retrieves multiple claims for a given token based on a provided condition specified by the predicate function.
    /// </summary>
    IEnumerable<Claim> GetClaims(string token, Func<Claim, bool> predicate);

    /// <summary>
    /// Retrieves all claims associated with the provided token.
    /// </summary>
    IEnumerable<Claim> GetClaims(string token);
}
