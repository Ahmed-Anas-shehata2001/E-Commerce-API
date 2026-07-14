using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Infrastructure.Identity.JWT;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace E_Commerce.Infrastructure.Identity.Services;

public sealed class TokenService : ITokenService
{
    private readonly JWTSettings _settings;
    private readonly SymmetricSecurityKey _securityKey;
    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    public TokenService(IOptions<JWTSettings> options)
    {
        _settings = options.Value;

        if (string.IsNullOrWhiteSpace(_settings.Secret))
            throw new InvalidOperationException(
                "JWT Secret is missing. Check the JWTSettings section in appsettings.json.");

        if (_settings.Secret.Length < 32)
            throw new InvalidOperationException(
                "JWT Secret must be at least 32 characters long.");

        _securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_settings.Secret));
    }

    public AccessToken GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var expiresAtUtc = DateTime.UtcNow.AddMinutes(_settings.AccessTokenMinutes);

        var credentials = new SigningCredentials(
            _securityKey,
            SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: expiresAtUtc,
            signingCredentials: credentials);

        return new AccessToken(
            _tokenHandler.WriteToken(jwt),
            expiresAtUtc);
    }

    public RefreshToken GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);

        return new RefreshToken(
            Convert.ToBase64String(randomBytes),
            DateTime.UtcNow.AddDays(_settings.RefreshTokenDays));
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string accessToken)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _settings.Issuer,

            ValidateAudience = true,
            ValidAudience = _settings.Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _securityKey,

            ValidateLifetime = false,

            ClockSkew = TimeSpan.Zero
        };

        try
        {
            var principal = _tokenHandler.ValidateToken(
                accessToken,
                validationParameters,
                out var validatedToken);

            if (validatedToken is not JwtSecurityToken jwt ||
                !jwt.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return principal;
        }
        catch (SecurityTokenException)
        {
            return null;
        }
        catch (ArgumentException)
        {
            return null;
        }
    }
}