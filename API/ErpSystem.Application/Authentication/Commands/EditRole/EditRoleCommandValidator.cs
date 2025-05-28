using FluentValidation;

namespace ErpSystem.Application.Authentication.Commands.EditRole;

public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
{
    public EditRoleCommandValidator()
    {
        RuleFor(x => x.RoleId).NotEmpty().WithMessage("Role ID is required.");
        RuleFor(x => x.Dto).NotNull().WithMessage("Role data is required.");
        RuleFor(x => x.Dto.Name)
            .NotEmpty()
            .WithMessage("Role name is required.")
            .MaximumLength(100)
            .WithMessage("Role name must not exceed 100 characters.");
        RuleFor(x => x.Dto.Description)
            .MaximumLength(500)
            .WithMessage("Role description must not exceed 500 characters.");
    }
}
