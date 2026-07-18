namespace E_Commerce.Application.Common.Contracts.Identity.Models
{
    // sealed  : prevent this class to be inherited ( good for models )

    // Note:   I'm using CQRS so  ChangePasswordCommand replaces  ChangePasswordRequest
    // LoginCommand replaces LoginRequest.
    //RegisterCommand replaces RegisterRequest.
    //ForgotPasswordCommand replaces ForgotPasswordRequest.
    public sealed class ChangePasswordRequest
    {
        public Guid UserId { get; init; } = default!;
        public string CurrentPassword { get; init; } = default!;
        public string NewPassword { get; init; } = default!;
        public string ConfirmNewPassword { get; init; } = default!;

    }

    public sealed class ForgotPasswordRequest
    {
        public string Email { get; init; } = default!;
    }

    public sealed class ResetPasswordRequest
    {
        public string Email { get; init; } = default!;
        public string Token { get; init; } = default!;
        public string NewPassword { get; init; } = default!;
        public string ConfirmNewPassword { get; init; } = default!;
    }
}
