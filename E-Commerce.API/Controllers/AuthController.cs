//using E_Commerce.Application.Common.Contracts.Identity.Models;
//using E_Commerce.Application.Features.Identity.Authentication;
//using E_Commerce.Application.Features.Identity.DTOs;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace E_Commerce.API.Controllers
//{
//    /// <summary>
//    /// Controller for handling authentication operations.
//    /// </summary>
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AuthController : ControllerBase
//    {
//        private readonly IMediator _mediator;

//        public AuthController(IMediator mediator)
//        {
//            _mediator = mediator;
//        }

//        /// <summary>
//        /// Register a new user.
//        /// </summary>
//        /// <param name="request">Registration request</param>
//        /// <returns>Auth result with tokens</returns>
//        [HttpPost("register")]
//        [AllowAnonymous]
//        public async Task<ActionResult<ApiResponseDto<AuthResponseDto>>> Register([FromBody] RegisterCommand command, CancellationToken ct)
//        {
//            try
//            {
//                if (!ModelState.IsValid)
//                    return BadRequest(new ApiResponseDto<AuthResponseDto>
//                    {
//                        IsSuccess = false,
//                        Message = "Invalid request data",
//                        Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()
//                    });


//                var result = await _mediator.Send(command, ct);

//                return Ok(new ApiResponseDto<AuthResponseDto>
//                {
//                    IsSuccess = result.Succeeded,
//                    Data = MapAuthResult(result),
//                    Message = result.Succeeded ? "Registration successful" : "Registration failed",
//                    Errors = result.Errors.ToList()
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new ApiResponseDto<AuthResponseDto>
//                {
//                    IsSuccess = false,
//                    Message = "An error occurred during registration",
//                    Errors = new List<string> { ex.Message }
//                });
//            }
//        }

//        /// <summary>
//        /// Login with email and password.
//        /// </summary>
//        /// <param name="request">Login request</param>
//        /// <returns>Auth result with tokens</returns>
//        [HttpPost("login")]
//        [AllowAnonymous]
//        public async Task<ActionResult<ApiResponseDto<AuthResponseDto>>> Login([FromBody] LoginDto request, CancellationToken ct)
//        {
//            try
//            {
//                if (!ModelState.IsValid)
//                    return BadRequest(new ApiResponseDto<AuthResponseDto>
//                    {
//                        IsSuccess = false,
//                        Message = "Invalid request data",
//                        Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()
//                    });

//                var command = new LoginCommand(request.Email, request.Password);
//                var result = await _mediator.Send(command, ct);

//                if (result.RequiresTwoFactor)
//                    return Ok(new ApiResponseDto<AuthResponseDto>
//                    {
//                        IsSuccess = false,
//                        Message = "Two-factor authentication required",
//                        Data = new AuthResponseDto { RequiresTwoFactor = true, UserId = result.UserId }
//                    });

//                return Ok(new ApiResponseDto<AuthResponseDto>
//                {
//                    IsSuccess = result.Succeeded,
//                    Data = MapAuthResult(result),
//                    Message = result.Succeeded ? "Login successful" : "Login failed",
//                    Errors = result.Errors.ToList()
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new ApiResponseDto<AuthResponseDto>
//                {
//                    IsSuccess = false,
//                    Message = "An error occurred during login",
//                    Errors = new List<string> { ex.Message }
//                });
//            }
//        }

//        /// <summary>
//        /// Refresh access token.
//        /// </summary>
//        /// <param name="request">Refresh token request</param>
//        /// <returns>New tokens</returns>
//        [HttpPost("refresh-token")]
//        [AllowAnonymous]
//        public async Task<ActionResult<ApiResponseDto<AuthResponseDto>>> RefreshToken([FromBody] RefreshTokenDto request, CancellationToken ct)
//        {
//            try
//            {
//                var command = new RefreshTokenCommand(request.RefreshToken);
//                var result = await _mediator.Send(command, ct);

//                return Ok(new ApiResponseDto<AuthResponseDto>
//                {
//                    IsSuccess = result.Succeeded,
//                    Data = MapAuthResult(result),
//                    Message = result.Succeeded ? "Token refreshed successfully" : "Token refresh failed",
//                    Errors = result.Errors.ToList()
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new ApiResponseDto<AuthResponseDto>
//                {
//                    IsSuccess = false,
//                    Message = "An error occurred during token refresh",
//                    Errors = new List<string> { ex.Message }
//                });
//            }
//        }

//        /// <summary>
//        /// Confirm user email.
//        /// </summary>
//        [HttpPost("confirm-email")]
//        [AllowAnonymous]
//        public async Task<ActionResult<ApiResponseDto<string>>> ConfirmEmail([FromBody] ConfirmEmailDto request, CancellationToken ct)
//        {
//            try
//            {
//                var command = new ConfirmEmailCommand(request.UserId, request.Token);
//                var result = await _mediator.Send(command, ct);

//                return Ok(new ApiResponseDto<string>
//                {
//                    IsSuccess = result.IsSuccess,
//                    Message = result.IsSuccess ? "Email confirmed successfully" : "Email confirmation failed",
//                    Errors = result.Error != null ? new List<string> { result.Error.Message } : new List<string>()
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new ApiResponseDto<string>
//                {
//                    IsSuccess = false,
//                    Message = "An error occurred during email confirmation",
//                    Errors = new List<string> { ex.Message }
//                });
//            }
//        }

//        /// <summary>
//        /// Resend confirmation email.
//        /// </summary>
//        [HttpPost("resend-confirmation-email")]
//        [AllowAnonymous]
//        public async Task<ActionResult<ApiResponseDto<string>>> ResendConfirmationEmail([FromBody] ResendEmailDto request, CancellationToken ct)
//        {
//            try
//            {
//                var command = new ResendConfirmationEmailCommand(request.Email);
//                var result = await _mediator.Send(command, ct);

//                return Ok(new ApiResponseDto<string>
//                {
//                    IsSuccess = result.IsSuccess,
//                    Message = result.IsSuccess ? "Confirmation email resent successfully" : "Failed to resend confirmation email",
//                    Errors = result.Error != null ? new List<string> { result.Error.Message } : new List<string>()
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new ApiResponseDto<string>
//                {
//                    IsSuccess = false,
//                    Message = "An error occurred while resending confirmation email",
//                    Errors = new List<string> { ex.Message }
//                });
//            }
//        }

//        /// <summary>
//        /// Change user password.
//        /// </summary>
//        [HttpPost("change-password")]
//        [Authorize]
//        public async Task<ActionResult<ApiResponseDto<string>>> ChangePassword([FromBody] ChangePasswordDto request, CancellationToken ct)
//        {
//            try
//            {
//                var command = new ChangePasswordCommand(
//               request.UserId,
//                     request.CurrentPassword,
//                  request.NewPassword,
//                 request.ConfirmNewPassword
//                    );

//                var result = await _mediator.Send(command, ct);

//                return Ok(new ApiResponseDto<string>
//                {
//                    IsSuccess = result.IsSuccess,
//                    Message = result.IsSuccess ? "Password changed successfully" : "Password change failed",
//                    Errors = result.Error != null ? new List<string> { result.Error.Message } : new List<string>()
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new ApiResponseDto<string>
//                {
//                    IsSuccess = false,
//                    Message = "An error occurred while changing password",
//                    Errors = new List<string> { ex.Message }
//                });
//            }
//        }

//        /// <summary>
//        /// Request password reset.
//        /// </summary>
//        [HttpPost("forgot-password")]
//        [AllowAnonymous]
//        public async Task<ActionResult<ApiResponseDto<string>>> ForgotPassword([FromBody] ForgotPasswordDto request, CancellationToken ct)
//        {
//            try
//            {
//                var command = new ForgotPasswordCommand(request.Email);
//                var result = await _mediator.Send(command, ct);

//                return Ok(new ApiResponseDto<string>
//                {
//                    IsSuccess = result.IsSuccess,
//                    Message = result.IsSuccess ? "Password reset email sent successfully" : "Password reset request failed",
//                    Errors = result.Error != null ? new List<string> { result.Error.Message } : new List<string>()
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new ApiResponseDto<string>
//                {
//                    IsSuccess = false,
//                    Message = "An error occurred while requesting password reset",
//                    Errors = new List<string> { ex.Message }
//                });
//            }
//        }

//        /// <summary>
//        /// Reset password with token.
//        /// </summary>
//        [HttpPost("reset-password")]
//        [AllowAnonymous]
//        public async Task<ActionResult<ApiResponseDto<string>>> ResetPassword([FromBody] ResetPasswordDto request, CancellationToken ct)
//        {
//            try
//            {
//                var command = new ResetPasswordCommand(
//                request.Email,
//                   request.Token,
//                 request.NewPassword,
//                    request.ConfirmNewPassword
//                       );

//                var result = await _mediator.Send(command, ct);

//                return Ok(new ApiResponseDto<string>
//                {
//                    IsSuccess = result.IsSuccess,
//                    Message = result.IsSuccess ? "Password reset successfully" : "Password reset failed",
//                    Errors = result.Error != null ? new List<string> { result.Error.Message } : new List<string>()
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new ApiResponseDto<string>
//                {
//                    IsSuccess = false,
//                    Message = "An error occurred while resetting password",
//                    Errors = new List<string> { ex.Message }
//                });
//            }
//        }

//        /// <summary>
//        /// Logout user.
//        /// </summary>
//        [HttpPost("logout")]
//        [Authorize]
//        public async Task<ActionResult<ApiResponseDto<string>>> Logout([FromBody] LogoutDto request, CancellationToken ct)
//        {
//            try
//            {
//                var command = new LogoutCommand(request.UserId);
//                var result = await _mediator.Send(command, ct);

//                return Ok(new ApiResponseDto<string>
//                {
//                    IsSuccess = result.IsSuccess,
//                    Message = result.IsSuccess ? "Logout successful" : "Logout failed",
//                    Errors = result.Error != null ? new List<string> { result.Error.Message } : new List<string>()
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new ApiResponseDto<string>
//                {
//                    IsSuccess = false,
//                    Message = "An error occurred during logout",
//                    Errors = new List<string> { ex.Message }
//                });
//            }
//        }

//        /// <summary>
//        /// Enable two-factor authentication.
//        /// </summary>
//        [HttpPost("2fa/enable")]
//        [Authorize]
//        public async Task<ActionResult<ApiResponseDto<TwoFactorSetupDto>>> EnableTwoFactor([FromBody] EnableTwoFactorDto request, CancellationToken ct)
//        {
//            try
//            {
//                var command = new EnableTwoFactorCommand(request.UserId);
//                var result = await _mediator.Send(command, ct);

//                if (!result.IsSuccess)
//                    return BadRequest(new ApiResponseDto<TwoFactorSetupDto>
//                    {
//                        IsSuccess = false,
//                        Errors = result.Error != null ? new List<string> { result.Error.Message } : new List<string>()
//                    });

//                var setupDto = new TwoFactorSetupDto
//                {
//                    SharedKey = result.Value.SharedKey,
//                    AuthenticatorUri = result.Value.AuthenticatorUri
//                };

//                return Ok(new ApiResponseDto<TwoFactorSetupDto>
//                {
//                    IsSuccess = true,
//                    Data = setupDto,
//                    Message = "2FA setup initiated successfully"
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new ApiResponseDto<TwoFactorSetupDto>
//                {
//                    IsSuccess = false,
//                    Message = "An error occurred while enabling 2FA",
//                    Errors = new List<string> { ex.Message }
//                });
//            }
//        }

//        /// <summary>
//        /// Verify two-factor authentication.
//        /// </summary>
//        [HttpPost("2fa/verify")]
//        [Authorize]
//        public async Task<ActionResult<ApiResponseDto<string>>> VerifyTwoFactor([FromBody] VerifyTwoFactorDto request, CancellationToken ct)
//        {
//            try
//            {
//                var command = new VerifyTwoFactorCommand(request.UserId, request.Code);
//                var result = await _mediator.Send(command, ct);

//                return Ok(new ApiResponseDto<string>
//                {
//                    IsSuccess = result.IsSuccess,
//                    Message = result.IsSuccess ? "2FA verification successful" : "2FA verification failed",
//                    Errors = result.Error != null ? new List<string> { result.Error.Message } : new List<string>()
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new ApiResponseDto<string>
//                {
//                    IsSuccess = false,
//                    Message = "An error occurred during 2FA verification",
//                    Errors = new List<string> { ex.Message }
//                });
//            }
//        }

//        /// <summary>
//        /// Login with two-factor code.
//        /// </summary>
//        [HttpPost("2fa/login")]
//        [AllowAnonymous]
//        public async Task<ActionResult<ApiResponseDto<AuthResponseDto>>> LoginWithTwoFactor([FromBody] LoginTwoFactorDto request, CancellationToken ct)
//        {
//            try
//            {
//                var command = new LoginWithTwoFactorCommand(request.UserId, request.Code);
//                var result = await _mediator.Send(command, ct);

//                return Ok(new ApiResponseDto<AuthResponseDto>
//                {
//                    IsSuccess = result.Succeeded,
//                    Data = MapAuthResult(result),
//                    Message = result.Succeeded ? "2FA login successful" : "2FA login failed",
//                    Errors = result.Errors.ToList()
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new ApiResponseDto<AuthResponseDto>
//                {
//                    IsSuccess = false,
//                    Message = "An error occurred during 2FA login",
//                    Errors = new List<string> { ex.Message }
//                });
//            }
//        }

//        /// <summary>
//        /// Disable two-factor authentication.
//        /// </summary>
//        [HttpPost("2fa/disable")]
//        [Authorize]
//        public async Task<ActionResult<ApiResponseDto<string>>> DisableTwoFactor([FromBody] DisableTwoFactorDto request, CancellationToken ct)
//        {
//            try
//            {
//                var command = new DisableTwoFactorCommand(request.UserId);
//                var result = await _mediator.Send(command, ct);

//                return Ok(new ApiResponseDto<string>
//                {
//                    IsSuccess = result.IsSuccess,
//                    Message = result.IsSuccess ? "2FA disabled successfully" : "2FA disabling failed",
//                    Errors = result.Error != null ? new List<string> { result.Error.Message } : new List<string>()
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new ApiResponseDto<string>
//                {
//                    IsSuccess = false,
//                    Message = "An error occurred while disabling 2FA",
//                    Errors = new List<string> { ex.Message }
//                });
//            }
//        }

//        private AuthResponseDto MapAuthResult(AuthResult result)
//        {
//            return new AuthResponseDto
//            {
//                Succeeded = result.Succeeded,
//                UserId = result.UserId,
//                Email = result.Email,
//                AccessToken = result.AccessToken,
//                AccessTokenExpiresAtUtc = result.AccessTokenExpiresAtUtc,
//                RefreshToken = result.RefreshToken,
//                RefreshTokenExpiresAtUtc = result.RefreshTokenExpiresAtUtc,
//                RequiresTwoFactor = result.RequiresTwoFactor,
//                RequiresEmailConfirmation = result.RequiresEmailConfirmation,
//                User = result.User != null ? MapUserInfo(result.User) : null,
//                Errors = result.Errors.ToList()
//            };
//        }

//        private UserInfoDto MapUserInfo(UserInfo user)
//        {
//            return new UserInfoDto
//            {
//                Id = user.Id,
//                Email = user.Email,
//                UserName = user.UserName,
//                FirstName = user.FirstName,
//                LastName = user.LastName,
//                PhoneNumber = user.PhoneNumber,
//                EmailConfirmed = user.EmailConfirmed,
//                TwoFactorEnabled = user.TwoFactorEnabled,
//                IsLockedOut = user.IsLockedOut,
//                Roles = user.Roles.ToList(),
//                Permissions = user.Permissions.ToList()
//            };
//        }
//    }
//}
