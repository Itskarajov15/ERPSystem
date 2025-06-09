using ErpSystem.Application.Common.Constants;
using FluentValidation;

namespace ErpSystem.Application.Authentication.Commands.EditRole;

public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
{
    public EditRoleCommandValidator()
    {
        RuleFor(x => x.RoleId).NotEmpty().WithMessage(RoleErrorKeys.RoleIdRequired);
        RuleFor(x => x.Dto).NotNull().WithMessage(RoleErrorKeys.RoleDataRequired);
        RuleFor(x => x.Dto.Name)
            .NotEmpty()
            .WithMessage(RoleErrorKeys.RoleNameRequired)
            .MaximumLength(100)
            .WithMessage(RoleErrorKeys.RoleNameTooLong);
        RuleFor(x => x.Dto.Description)
            .MaximumLength(500)
            .WithMessage(RoleErrorKeys.RoleDescriptionTooLong);
    }
}
