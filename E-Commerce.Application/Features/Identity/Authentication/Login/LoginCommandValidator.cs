using FluentValidation;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Validator for LoginCommand.
    /// </summary>
   public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
     public LoginCommandValidator()
  {
  RuleFor(x => x.Email)
   .NotEmpty().WithMessage("Email is required.")
.EmailAddress().WithMessage("Email format is invalid.");

     RuleFor(x => x.Password)
    .NotEmpty().WithMessage("Password is required.");
      }
    }
}
