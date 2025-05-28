using FluentValidation;

namespace ErpSystem.Application.Authentication.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Role name is required.")
            .MinimumLength(3)
            .WithMessage("Role name must be at least 3 characters.")
            .MaximumLength(50)
            .WithMessage("Role name must not exceed 50 characters.")
            .Matches("^[a-zA-Z0-9_-]*$")
            .WithMessage("Role name can only contain letters, numbers, underscores, and hyphens.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}
