using ErpSystem.Application.Common.Constants;
using FluentValidation;

namespace ErpSystem.Application.PaymentMethods.Commands.CreatePaymentMethod;

public class CreatePaymentMethodCommandValidator : AbstractValidator<CreatePaymentMethodCommand>
{
    public CreatePaymentMethodCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(PaymentMethodErrorKeys.NameRequired)
            .MaximumLength(50)
            .WithMessage(PaymentMethodErrorKeys.NameTooLong);
    }
}
