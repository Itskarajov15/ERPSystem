using ErpSystem.Application.Common.Constants;
using FluentValidation;

namespace ErpSystem.Application.Authentication.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(RoleErrorKeys.RoleNameRequired)
            .MinimumLength(3)
            .WithMessage(RoleErrorKeys.RoleNameTooShort)
            .MaximumLength(50)
            .WithMessage(RoleErrorKeys.RoleNameTooLong)
            .Matches("^[a-zA-Z0-9_-]*$")
            .WithMessage(RoleErrorKeys.RoleNameInvalidCharacters);

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage(RoleErrorKeys.RoleDescriptionTooLong)
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.PermissionIds).NotNull().WithMessage(RoleErrorKeys.PermissionIdsRequired);
    }
}
