using FluentValidation;

namespace ErpSystem.Application.UnitsOfMeasures.Commands.UpdateUnitOfMeasure;

public class UpdateUnitOfMeasureCommandValidator : AbstractValidator<UpdateUnitOfMeasureCommand>
{
    public UpdateUnitOfMeasureCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters.");
    }
}
