using ErpSystem.Domain.Entities.Sales;
using FluentValidation;

namespace ErpSystem.Application.PaymentMethods.Commands.UpdatePaymentMethod;

public class UpdatePaymentMethodCommandValidator : AbstractValidator<PaymentMethod>
{
    public UpdatePaymentMethodCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Payment method name is required.")
            .MaximumLength(50)
            .WithMessage("Payment method name must not exceed 50 characters.");
    }
}
