using FluentValidation;

namespace E_Commerce.Application.Features.Identity.Roles
{
    /// <summary>
    /// Validator for CreateRoleCommand.
    /// </summary>
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
   {
 public CreateRoleCommandValidator()
  {
   RuleFor(x => x.Name)
          .NotEmpty().WithMessage("Role name is required.")
   .MinimumLength(3).WithMessage("Role name must be at least 3 characters.")
    .MaximumLength(100).WithMessage("Role name cannot exceed 100 characters.")
   .Matches(@"^[a-zA-Z0-9_-]+$").WithMessage("Role name can only contain alphanumeric characters, hyphens, and underscores.");

        RuleFor(x => x.Description)
   .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
       .When(x => !string.IsNullOrEmpty(x.Description));
      }
    }
}
