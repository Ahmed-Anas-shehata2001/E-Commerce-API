using E_Commerce.Infrastructure.Identity.Identity_Entites;
using E_Commerce.Infrastructure.Identity.Identity_Entites.UserSecurityEvents;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; }



    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



    // Optional: soft-delete instead of hard delete
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAtUtc { get; set; }
    public string? DeletedByUserId { get; set; }  // to know who performed the deletion



    //  ********* Auditing********** // 
    public DateTime? UpdatedAtUtc { get; set; }

    public DateTime? LastLoginAtUtc { get; set; }



    // Navigation Properties
    public ICollection<RefreshTokenEntity> RefreshTokens { get; set; } = [];
    public ICollection<UserSession> Sessions { get; set; } = [];
    public ICollection<UserLoginHistory> LoginHistories { get; set; } = [];
    public ICollection<UserSecurityEvent> SecurityEvents { get; set; } = [];
    public ICollection<UserTwoFactorMethod> TwoFactorMethods { get; set; } = [];
}