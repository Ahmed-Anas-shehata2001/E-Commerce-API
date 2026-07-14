using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

using static E_Commerce.Application.Common.Contracts.Identity.Models.TwoFactorModels;

namespace E_Commerce.Application.Common.Contracts.Identity
{
    // handle Authentication use cases
    public interface IAuthenticationService
    {

        // Handles Authentication use cases (sign up, sign in, tokens, password + 2FA lifecycle)
      
            Task<AuthResult> RegisterAsync(RegisterRequest request, CancellationToken ct = default);
            Task<Result> ConfirmEmailAsync(string userId, string token, CancellationToken ct = default);
            Task<Result> ResendConfirmationEmailAsync(string email, CancellationToken ct = default);

            Task<AuthResult> LoginAsync(LoginRequest request, CancellationToken ct = default);
            Task<AuthResult> RefreshTokenAsync(string refreshToken, CancellationToken ct = default);
            Task<Result> LogoutAsync(Guid userId, CancellationToken ct = default);

            Task<Result> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken ct = default);
            Task<Result> ForgotPasswordAsync(string email, CancellationToken ct = default);
            Task<Result> ResetPasswordAsync(ResetPasswordRequest request, CancellationToken ct = default);

            // External login (Google, Facebook, Microsoft, ...). Provider token is verified
            // server-side; if no local account exists yet one is provisioned automatically.
            Task<AuthResult> ExternalLoginAsync(ExternalLoginRequest request, CancellationToken ct = default);

            // Two-factor authentication (TOTP authenticator apps)
            Task<Result<TwoFactorSetupResult>> EnableTwoFactorAsync(string userId, CancellationToken ct = default);
            Task<Result> VerifyTwoFactorAsync(TwoFactorVerifyRequest request, CancellationToken ct = default);
            Task<AuthResult> LoginWithTwoFactorAsync(TwoFactorLoginRequest request, CancellationToken ct = default);
            Task<Result> DisableTwoFactorAsync(string userId, CancellationToken ct = default);
        


     
    }
}
