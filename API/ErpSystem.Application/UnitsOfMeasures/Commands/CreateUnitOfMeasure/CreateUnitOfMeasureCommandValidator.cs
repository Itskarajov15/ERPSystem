using ErpSystem.Application.Common.Constants;
using FluentValidation;

namespace ErpSystem.Application.UnitsOfMeasures.Commands.CreateUnitOfMeasure;

public class CreateUnitOfMeasureCommandValidator : AbstractValidator<CreateUnitOfMeasureCommand>
{
    public CreateUnitOfMeasureCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(UnitOfMeasureErrorKeys.NameRequired)
            .MaximumLength(50)
            .WithMessage(UnitOfMeasureErrorKeys.NameTooLong);
    }
}
