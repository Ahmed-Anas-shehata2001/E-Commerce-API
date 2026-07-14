using FluentValidation;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Validator for ChangePasswordCommand.
    /// </summary>
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
  {
      public ChangePasswordCommandValidator()
    {
     RuleFor(x => x.UserId)
        .NotEmpty().WithMessage("User ID is required.");

           RuleFor(x => x.CurrentPassword)
  .NotEmpty().WithMessage("Current password is required.");

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
