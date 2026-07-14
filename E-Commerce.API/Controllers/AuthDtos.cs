//namespace E_Commerce.API.Controllers
//{
//    /// <summary>
//    /// DTO for user registration request.
//    /// </summary>
//    public class RegisterDto
//  {
//   public string Email { get; set; } = default!;
//       public string Password { get; set; } = default!;
//public string ConfirmPassword { get; set; } = default!;
//   public string FirstName { get; set; } = default!;
//      public string LastName { get; set; } = default!;
//   public string? PhoneNumber { get; set; }
//  }

//    /// <summary>
//    /// DTO for user login request.
//    /// </summary>
//   public class LoginDto
//    {
//public string Email { get; set; } = default!;
// public string Password { get; set; } = default!;
//    }

///// <summary>
//    /// DTO for refresh token request.
//   /// </summary>
//public class RefreshTokenDto
//     {
//    public string RefreshToken { get; set; } = default!;
//   }

//    /// <summary>
//    /// DTO for confirm email request.
//    /// </summary>
//  public class ConfirmEmailDto
//    {
// public string UserId { get; set; } = default!;
//     public string Token { get; set; } = default!;
//   }

//    /// <summary>
//    /// DTO for resend confirmation email request.
//    /// </summary>
//     public class ResendEmailDto
// {
//    public string Email { get; set; } = default!;
//  }

//   /// <summary>
///// DTO for change password request.
//     /// </summary>
//   public class ChangePasswordDto
//    {
//     public string UserId { get; set; } = default!;
//       public string CurrentPassword { get; set; } = default!;
//   public string NewPassword { get; set; } = default!;
//    public string ConfirmNewPassword { get; set; } = default!;
//   }

//    /// <summary>
//    /// DTO for forgot password request.
//    /// </summary>
//   public class ForgotPasswordDto
// {
//   public string Email { get; set; } = default!;
//   }

//    /// <summary>
//   /// DTO for reset password request.
//    /// </summary>
//    public class ResetPasswordDto
// {
//    public string Email { get; set; } = default!;
//     public string Token { get; set; } = default!;
//   public string NewPassword { get; set; } = default!;
//      public string ConfirmNewPassword { get; set; } = default!;
//     }

//   /// <summary>
//    /// DTO for logout request.
//   /// </summary>
//    public class LogoutDto
//  {
//  public string UserId { get; set; } = default!;
// }

//    /// <summary>
//  /// DTO for enable two-factor request.
//    /// </summary>
// public class EnableTwoFactorDto
//    {
//    public string UserId { get; set; } = default!;
//  }

//    /// <summary>
//   /// DTO for verify two-factor request.
//  /// </summary>
//  public class VerifyTwoFactorDto
//    {
//  public string UserId { get; set; } = default!;
//   public string Code { get; set; } = default!;
//   }

//  /// <summary>
//    /// DTO for two-factor login request.
//  /// </summary>
//  public class LoginTwoFactorDto
// {
//   public string UserId { get; set; } = default!;
//      public string Code { get; set; } = default!;
//   }

//   /// <summary>
//    /// DTO for disable two-factor request.
//  /// </summary>
//  public class DisableTwoFactorDto
// {
//   public string UserId { get; set; } = default!;
//   }
//}
