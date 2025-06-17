using ErpSystem.Application.Common.Constants;
using ErpSystem.Domain.Entities.Sales;
using FluentValidation;

namespace ErpSystem.Application.PaymentMethods.Commands.UpdatePaymentMethod;

public class UpdatePaymentMethodCommandValidator : AbstractValidator<PaymentMethod>
{
    public UpdatePaymentMethodCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(PaymentMethodErrorKeys.NameRequired)
            .MaximumLength(50)
            .WithMessage(PaymentMethodErrorKeys.NameTooLong);
    }
}
