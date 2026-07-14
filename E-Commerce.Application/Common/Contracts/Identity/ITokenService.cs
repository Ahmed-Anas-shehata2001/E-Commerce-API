
using System.Security.Claims;


namespace E_Commerce.Application.Common.Contracts.Identity
{

    /*
     Imagine a future requirement  Suppose your company says:
 "We no longer want JWT."  , we want

    OAuth
    OpenIddict
    IdentityServer
    Azure AD
    Auth0

    Where is the JWT code?

    Only here:

    TokenService

    You replace this implementation.

    Everything else stays exactly the same.

    That's why we abstract it.
     */

    public record AccessToken(
    string Token,
    DateTime ExpiresAtUtc);

    public record RefreshToken(
        string Token,
        DateTime ExpiresAtUtc);

    public interface ITokenService
    {
        AccessToken GenerateAccessToken(IEnumerable<Claim> claims);
        RefreshToken GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string accessToken);
    }
}
