using FluentValidation;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Validator for RegisterCommand.
    /// </summary>
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email)
          .NotEmpty().WithMessage("Email is required.")
        .EmailAddress().WithMessage("Email format is invalid.");

            RuleFor(x => x.Password)
    .NotEmpty().WithMessage("Password is required.")
          .MinimumLength(8).WithMessage("Password must be at least 8 characters.");
            /* //.Matches(@"[A-Z]").WithMessage("Password must contain an uppercase letter.")
            //.Matches(@"[a-z]").WithMessage("Password must contain a lowercase letter.")
            //.Matches(@"[0-9]").WithMessage("Password must contain a digit.")
            //.Matches(@"[!@#$%^&*()_+\-=\[\]{};:,.<>?]").WithMessage("Password must contain a special character.");  */

            RuleFor(x => x.ConfirmPassword)
                 .Equal(x => x.Password).WithMessage("Passwords do not match.");

            RuleFor(x => x.FirstName)
              .NotEmpty().WithMessage("First name is required.")
         .MaximumLength(100).WithMessage("First name cannot exceed 100 characters.");

            RuleFor(x => x.LastName)
                    .NotEmpty().WithMessage("Last name is required.")
                     .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters.");

            RuleFor(x => x.PhoneNumber)
              .Matches(@"^\d{10,15}$").WithMessage("Phone number must be 10-15 digits.")
               .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        }
    }
}
