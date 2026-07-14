using FluentValidation;

namespace E_Commerce.Application.Features.Identity.Authentication
{
 /// <summary>
    /// Validator for ResetPasswordCommand.
    /// </summary>
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
     public ResetPasswordCommandValidator()
     {
 RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email is required.")
    .EmailAddress().WithMessage("Email format is invalid.");

 RuleFor(x => x.Token)
       .NotEmpty().WithMessage("Reset token is required.");

    RuleFor(x => x.NewPassword)
      .NotEmpty().WithMessage("New password is required.")
          .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
      .Matches(@"[A-Z]").WithMessage("Password must contain an uppercase letter.")
  .Matches(@"[a-z]").WithMessage("Password must contain a lowercase letter.")
    .Matches(@"[0-9]").WithMessage("Password must contain a digit.")
     .Matches(@"[!@#$%^&*()_+\-=\[\]{};:,.<>?]").WithMessage("Password must contain a special character.");

 RuleFor(x => x.ConfirmNewPassword)
      .Equal(x => x.NewPassword).WithMessage("Passwords do not match.");
   }
    }
}
