using E_Commerce.Application.Common.Constants;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Infrastructure.Identity.Identity_Entites;
using E_Commerce.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static E_Commerce.Application.Common.Contracts.Identity.Models.TwoFactorModels;

namespace E_Commerce.Infrastructure.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;
        private readonly IEnumerable<IExternalAuthValidator> _externalAuthValidators;

        public AuthenticationService(
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext context,
            ILogger<AuthenticationService> logger,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            ITokenService tokenService,
            IEmailSender emailSender,
            IEnumerable<IExternalAuthValidator> externalAuthValidators)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _emailSender = emailSender;
            _externalAuthValidators = externalAuthValidators;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<AuthResult> RegisterAsync(RegisterRequest request, CancellationToken ct = default)
        {

            var existing = await _userManager.FindByEmailAsync(request.Email);
            if (existing is not null)
                return AuthResult.Failure("An account with this email already exists.");

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
                return AuthResult.Failure(createResult.Errors.Select(e => e.Description).ToArray());

            // add default role to the user
            var roleResult = await _userManager.AddToRoleAsync(user, Roles.Customer);

            if (!roleResult.Succeeded)
            {
                return AuthResult.Failure(roleResult.Errors.Select(x => x.Description).ToArray());
            }


            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            _logger.LogInformation("Received token: {Token}", token); //


            try
            {

                // Registration succeeds even if sending the email fails. because you cached the exception
                // The user can request another confirmation email later.
                await SendConfirmationEmailAsync(user, token, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send confirmation email.");
            }

            return new AuthResult
            {
                Succeeded = true,
                UserId = user.Id,
                Email = user.Email,
                RequiresEmailConfirmation = true
            };
        }


        public async Task<Result> ConfirmEmailAsync(
    string userId,
    string token,
    CancellationToken ct = default)
        {
            _logger.LogInformation("*************************************** confirm Email ***************************************");
            _logger.LogInformation("================================");
            _logger.LogInformation("UserId: {UserId}", userId);
            _logger.LogInformation("Received Token: {Token}", token);
            _logger.LogInformation("Length: {Length}", token.Length);
            _logger.LogInformation("================================");

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure("User not found.");

            try
            {
                var decodedToken = Encoding.UTF8.GetString(
                    WebEncoders.Base64UrlDecode(token));

                _logger.LogInformation("Decoded Token:");
                _logger.LogInformation(decodedToken);

                var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        _logger.LogError(error.Description);
                }

                return result.Succeeded
                    ? Result.Success()
                    : Result.Failure(result.Errors.Select(e => e.Description).ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while confirming email.");
                return Result.Failure(ex.Message);
            }
        }



        public async Task<Result> ResendConfirmationEmailAsync(
    string email,
    CancellationToken ct = default)
        {
            _logger.LogInformation("******************************* Resend Confirmation Email *******************************");
            _logger.LogInformation("Email: {Email}", email);

            var user = await _userManager.FindByEmailAsync(email);
            // Don't leak whether the account exists

            if (user == null)
            {
                _logger.LogWarning("User not found.");
                return Result.Success();
            }

            _logger.LogInformation("User found. Id: {Id}", user.Id);

            var confirmed = await _userManager.IsEmailConfirmedAsync(user);

            _logger.LogInformation("Email Confirmed: {Confirmed}", confirmed);

            if (confirmed)
            {
                _logger.LogInformation("Email already confirmed.");
                return Result.Success();
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            _logger.LogInformation("Generated Token:");
            _logger.LogInformation(token);

            await SendConfirmationEmailAsync(user, token, ct);

            _logger.LogInformation("Confirmation email sent.");

            return Result.Success();
        }

        public async Task<AuthResult> LoginAsync(LoginRequest request, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return AuthResult.Failure("Invalid email or password.");

            var checkResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);

            if (checkResult.IsLockedOut)
                return AuthResult.Failure("Account is locked. Try again later.");
            if (checkResult.IsNotAllowed)
                return AuthResult.Failure("Email is not confirmed yet.");
            if (!checkResult.Succeeded)
                return AuthResult.Failure("Invalid email or password.");

            if (await _userManager.GetTwoFactorEnabledAsync(user))
                return AuthResult.TwoFactorRequired(user.Id);

            return await SignInAsync(user, ct);

        }

        public async Task<AuthResult> RefreshTokenAsync(string refreshToken, CancellationToken ct = default)
        {
            var tokenHash = TokenHasher.ComputeHash(refreshToken);

            var storedToken = await _context.RefreshTokens
                .Include(x => x.User)
                .Include(x => x.Session)
                .SingleOrDefaultAsync(x => x.TokenHash == tokenHash, ct);

            if (storedToken is null || !storedToken.IsActive)
                return AuthResult.Failure("Invalid or expired refresh token.");

            // revoke old token (rotation)
            storedToken.RevokedAtUtc = DateTime.UtcNow;
            storedToken.RevocationReason = "Rotated";
            storedToken.LastUsedAtUtc = DateTime.UtcNow;

            return await IssueTokensAsync(storedToken.User, storedToken.Session, storedToken, ct);
        }

        public async Task<Result> LogoutAsync(Guid userId, CancellationToken ct = default)
        {
            var sessions = await _context.UserSessions
                .Include(x => x.RefreshTokens)
                .Where(x => x.UserId == userId && !x.IsRevoked)
                .ToListAsync(ct);

            if (!sessions.Any())
                return Result.Success();

            foreach (var session in sessions)
            {
                session.IsRevoked = true;
                session.RevokedAtUtc = DateTime.UtcNow;

                foreach (var token in session.RefreshTokens.Where(t => t.IsActive))
                {
                    token.RevokedAtUtc = DateTime.UtcNow;
                    token.RevocationReason = "Logout";
                }
            }

            await _context.SaveChangesAsync(ct);

            return Result.Success();
        }

        public async Task<Result> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken ct = default)
        {
            if (request.NewPassword != request.ConfirmNewPassword)
                return Result.Failure("New passwords do not match.");

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
                return Result.Failure("User not found.");

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<Result> ForgotPasswordAsync(string email, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            // Don't leak whether the account exists
            if (user is null || !await _userManager.IsEmailConfirmedAsync(user))
                return Result.Success();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            await _emailSender.SendAsync(
                user.Email!,
                "Reset your password",
                $"Use this token to reset your password: {encodedToken}",
                ct);

            return Result.Success();
        }

        public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request, CancellationToken ct = default)
        {
            if (request.NewPassword != request.ConfirmNewPassword)
                return Result.Failure("New passwords do not match.");

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return Result.Failure("Invalid request.");

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);

            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<AuthResult> ExternalLoginAsync(ExternalLoginRequest request, CancellationToken ct = default)
        {
            var validator = _externalAuthValidators.FirstOrDefault(v =>
                string.Equals(v.Provider, request.Provider, StringComparison.OrdinalIgnoreCase));

            if (validator is null)
                return AuthResult.Failure($"Provider '{request.Provider}' is not supported.");

            var payload = await validator.ValidateAsync(request.ProviderToken, ct);
            if (payload is null)
                return AuthResult.Failure("Invalid external login token.");

            var user = await _userManager.FindByLoginAsync(request.Provider, payload.ProviderKey);

            if (user is null)
            {
                // Fall back to matching by email so a user who registered normally can
                // also sign in with a matching external provider, then link the login.
                user = await _userManager.FindByEmailAsync(payload.Email);

                if (user is null)
                {
                    user = new ApplicationUser
                    {
                        UserName = payload.Email,
                        Email = payload.Email,
                        FirstName = payload.FirstName ?? string.Empty,
                        LastName = payload.LastName ?? string.Empty,
                        EmailConfirmed = true
                    };

                    var createResult = await _userManager.CreateAsync(user);
                    if (!createResult.Succeeded)
                        return AuthResult.Failure(createResult.Errors.Select(e => e.Description).ToArray());
                }

                var addLoginResult = await _userManager.AddLoginAsync(user,
                    new UserLoginInfo(request.Provider, payload.ProviderKey, request.Provider));

                if (!addLoginResult.Succeeded)
                    return AuthResult.Failure(addLoginResult.Errors.Select(e => e.Description).ToArray());
            }
            return await SignInAsync(user, ct);

        }

        public async Task<Result<TwoFactorSetupResult>> EnableTwoFactorAsync(string userId, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return Result<TwoFactorSetupResult>.Failure("User not found.");

            await _userManager.ResetAuthenticatorKeyAsync(user);
            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);

            var email = await _userManager.GetEmailAsync(user);
            var authenticatorUri =
                $"otpauth://totp/{Uri.EscapeDataString("E-Commerce")}:{Uri.EscapeDataString(email!)}" +
                $"?secret={unformattedKey}&issuer={Uri.EscapeDataString("E-Commerce")}&digits=6";

            return Result<TwoFactorSetupResult>.Success(new TwoFactorSetupResult
            {
                SharedKey = unformattedKey!,
                AuthenticatorUri = authenticatorUri
            });
        }

        public async Task<Result> VerifyTwoFactorAsync(TwoFactorVerifyRequest request, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
                return Result.Failure("User not found.");

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(
                user, _userManager.Options.Tokens.AuthenticatorTokenProvider, request.Code);

            if (!isValid)
                return Result.Failure("Invalid authenticator code.");

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            return Result.Success();
        }

        public async Task<AuthResult> LoginWithTwoFactorAsync(TwoFactorLoginRequest request, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
                return AuthResult.Failure("User not found.");

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(
                user, _userManager.Options.Tokens.AuthenticatorTokenProvider, request.Code);

            if (!isValid)
                return AuthResult.Failure("Invalid authenticator code.");

            return await SignInAsync(user, ct);
        }

        public async Task<Result> DisableTwoFactorAsync(string userId, CancellationToken ct = default)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return Result.Failure("User not found.");

            await _userManager.SetTwoFactorEnabledAsync(user, false);
            return Result.Success();
        }

        // ---------- helpers ----------


        private async Task<IReadOnlyList<string>> GetPermissionNamesAsync(IEnumerable<string> roleNames)
        {
            var permissions = new HashSet<string>();
            foreach (var roleName in roleNames)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role is null) continue;

                var claims = await _roleManager.GetClaimsAsync(role);
                foreach (var claim in claims.Where(c => c.Type == IdentityClaimTypes.Permission))
                    permissions.Add(claim.Value);
            }
            return permissions.ToArray();
        }



        /*
            * Issues a new authentication token pair.
            *
            * 1. Generate a new access token.
            * 2. Generate a new refresh token.
            * 3. Replace the user's previous refresh token in the database (rotation).
            * 4. Persist the new refresh token and its expiration.
            * 5. Return the new access token and refresh token to the client.
         */
        private async Task<AuthResult> IssueTokensAsync(
    ApplicationUser user,
    UserSession session,
    RefreshTokenEntity? previousToken,
    CancellationToken ct)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
    {
        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new(ClaimTypes.Email, user.Email!),
        new(ClaimTypes.Name, user.UserName!)
    };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var accessToken = _tokenService.GenerateAccessToken(claims);

            var refreshToken = _tokenService.GenerateRefreshToken();

            var ip = _httpContextAccessor.HttpContext?
            .Connection
            .RemoteIpAddress?
            .ToString();

            var refreshTokenEntity = new RefreshTokenEntity
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                SessionId = session.Id,
                TokenHash = TokenHasher.ComputeHash(refreshToken.Token),
                CreatedAtUtc = DateTime.UtcNow,
                ExpiresAtUtc = refreshToken.ExpiresAtUtc,
                CreatedByIp = ip
            };

            if (previousToken != null)
            {
                previousToken.ReplacedByToken = refreshTokenEntity;
            }

            _context.RefreshTokens.Add(refreshTokenEntity);

            await _context.SaveChangesAsync(ct);

            var userInfo = new UserInfo
            {
                Id = user.Id,
                Email = user.Email!,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                EmailConfirmed = user.EmailConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                IsLockedOut = await _userManager.IsLockedOutAsync(user),
                Roles = roles.ToArray(),
                Permissions = await GetPermissionNamesAsync(roles)
            };

            return AuthResult.Success(
                userInfo,
                accessToken.Token,
                accessToken.ExpiresAtUtc,
                refreshToken.Token,
                refreshToken.ExpiresAtUtc);
        }


        private async Task SendConfirmationEmailAsync(
    ApplicationUser user,
    string token,
    CancellationToken ct)
        {
            _logger.LogInformation("Original token: {Token}", token);

            var encodedToken = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(token));

            _logger.LogInformation("Encoded token: {EncodedToken}", encodedToken);



            // Build the real confirmation link using your frontend URL / IUrlHelper as needed.
            var baseUrl = _configuration["Application:BaseUrl"];
            var confirmationLink =
                $"{baseUrl}/api/authentication/confirm-email?userId={user.Id}&token={encodedToken}";

            _logger.LogInformation("Confirmation link: {Link}", confirmationLink);



            var body = $"""
        <h2>Welcome!</h2>

        <p>Please confirm your email by clicking the link below.</p>

        <a href="{confirmationLink}">
            Confirm Email
        </a>
        """;

            await _emailSender.SendAsync(
                user.Email!,
                "Confirm your email",
                body,
                ct);
        }


        private async Task<UserSession> CreateSessionAsync(
    ApplicationUser user,
    CancellationToken ct)
        {
            var session = new UserSession
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,

                DeviceName = "Unknown Device",
                UserAgent = null,
                IpAddress = null,

                OperatingSystem = null,
                Browser = null,

                CreatedAtUtc = DateTime.UtcNow,
                LastActivityAtUtc = DateTime.UtcNow
            };

            _context.UserSessions.Add(session);

            await _context.SaveChangesAsync(ct);

            return session;
        }

        private async Task<AuthResult> SignInAsync(
    ApplicationUser user,
    CancellationToken ct)
        {
            var session = await CreateSessionAsync(user, ct);

            return await IssueTokensAsync(user, session, null, ct);
        }
    }




    public static class TokenHasher
    {
        public static string ComputeHash(string token)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(token);

            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(token));

            return Convert.ToHexString(hash);
        }
    }
}

