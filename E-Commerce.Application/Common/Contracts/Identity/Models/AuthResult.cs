

namespace E_Commerce.Application.Common.Contracts.Identity.Models
{
    public class AuthResult
    {

        /*
         AuthResult ✅

                //This is the result of an authentication operation.

                Example: POST /login
                returns : AuthResult
         */
        public bool Succeeded { get; init; }
        public Guid? UserId { get; init; }
        public string? Email { get; init; }
        public string? AccessToken { get; init; }
        public DateTime? AccessTokenExpiresAtUtc { get; init; }
        public string? RefreshToken { get; init; }
        public DateTime? RefreshTokenExpiresAtUtc { get; init; }
        public bool RequiresTwoFactor { get; init; }
        public bool RequiresEmailConfirmation { get; init; }
        public UserInfo? User { get; init; }
        public IReadOnlyList<string> Errors { get; init; } = Array.Empty<string>();

        public static AuthResult Success(UserInfo user, string accessToken, DateTime accessTokenExpiresAtUtc,
            string refreshToken, DateTime refreshTokenExpiresAtUtc) => new()
            {
                Succeeded = true,
                UserId = user.Id,
                Email = user.Email,
                User = user,
                AccessToken = accessToken,
                AccessTokenExpiresAtUtc = accessTokenExpiresAtUtc,
                RefreshToken = refreshToken,
                RefreshTokenExpiresAtUtc = refreshTokenExpiresAtUtc
            };

        public static AuthResult Failure(params string[] errors) => new()
        {
            Succeeded = false,
            Errors = errors
        };

        public static AuthResult TwoFactorRequired(Guid userId) => new()
        {
            Succeeded = false,
            RequiresTwoFactor = true,
            UserId = userId
        };
    }
}
