using FluentValidation;

namespace E_Commerce.Application.Features.Identity.Users
{
    /// <summary>
    /// Validator for UpdateUserCommand.
    /// </summary>
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                    .NotEmpty().WithMessage("User ID is required.");

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
