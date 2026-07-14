//namespace E_Commerce.Application.Features.Identity.DTOs
//{
//    /// <summary>
//    /// DTO for authentication response.
//    /// </summary>
//    public class AuthResponseDto
//    {
//     public bool Succeeded { get; set; }
// public Guid? UserId { get; set; }
//     public string? Email { get; set; }
// public string? AccessToken { get; set; }
//        public DateTime? AccessTokenExpiresAtUtc { get; set; }
// public string? RefreshToken { get; set; }
//        public DateTime? RefreshTokenExpiresAtUtc { get; set; }
//  public bool RequiresTwoFactor { get; set; }
//        public bool RequiresEmailConfirmation { get; set; }
//  public UserInfoDto? User { get; set; }
//  public List<string> Errors { get; set; } = new();
//    }

//    /// <summary>
//    /// DTO for user information response.
// /// </summary>
//    public class UserInfoDto
//   {
//public string Id { get; set; } = default!;
//        public string Email { get; set; } = default!;
//     public string? UserName { get; set; }
//  public string FirstName { get; set; } = default!;
//      public string LastName { get; set; } = default!;
// public string? PhoneNumber { get; set; }
//      public bool EmailConfirmed { get; set; }
//  public bool TwoFactorEnabled { get; set; }
//        public bool IsLockedOut { get; set; }
//     public List<string> Roles { get; set; } = new();
//        public List<string> Permissions { get; set; } = new();
//    }

//    /// <summary>
//    /// DTO for role response.
//    /// </summary>
//    public class RoleResponseDto
//    {
//   public string Id { get; set; } = default!;
//     public string Name { get; set; } = default!;
//       public string? Description { get; set; }
//      public List<string> Permissions { get; set; } = new();
//   }

//   /// <summary>
//    /// DTO for paginated results.
//    /// </summary>
//   public class PaginatedResponseDto<T>
//   {
//  public List<T> Items { get; set; } = new();
//      public int PageNumber { get; set; }
//     public int PageSize { get; set; }
//  public int TotalCount { get; set; }
//    public int TotalPages { get; set; }
//       public bool HasPreviousPage { get; set; }
//      public bool HasNextPage { get; set; }
//   }

//    /// <summary>
//    /// DTO for API response with result.
//    /// </summary>
//  public class ApiResponseDto<T>
//    {
//public bool IsSuccess { get; set; }
//    public T? Data { get; set; }
//     public string? Message { get; set; }
//        public List<string> Errors { get; set; } = new();
//    }

//  /// <summary>
//    /// DTO for 2FA setup response.
//    /// </summary>
//    public class TwoFactorSetupDto
//    {
//     public string SharedKey { get; set; } = default!;
//       public string AuthenticatorUri { get; set; } = default!;
//     public string QrCodeUrl { get; set; } = default!;
//  }
//}
