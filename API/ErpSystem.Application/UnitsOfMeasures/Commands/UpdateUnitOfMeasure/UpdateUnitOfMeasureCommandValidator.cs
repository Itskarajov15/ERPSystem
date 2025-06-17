using ErpSystem.Application.Common.Constants;
using FluentValidation;

namespace ErpSystem.Application.UnitsOfMeasures.Commands.UpdateUnitOfMeasure;

public class UpdateUnitOfMeasureCommandValidator : AbstractValidator<UpdateUnitOfMeasureCommand>
{
    public UpdateUnitOfMeasureCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(UnitOfMeasureErrorKeys.NameRequired)
            .MaximumLength(50)
            .WithMessage(UnitOfMeasureErrorKeys.NameTooLong);
    }
}
