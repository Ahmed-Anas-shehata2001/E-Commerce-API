namespace E_Commerce.Infrastructure.Identity;

/// <summary>
/// Defines custom claim types used throughout the application.
/// </summary>
public static class IdentityClaimTypes
{
    /// <summary>
    /// Represents a permission assigned to the authenticated user.
    /// </summary>
    public const string Permission = "permission";

    /// <summary>
    /// Represents the current authenticated session.
    /// </summary>
    public const string SessionId = "session_id";
}