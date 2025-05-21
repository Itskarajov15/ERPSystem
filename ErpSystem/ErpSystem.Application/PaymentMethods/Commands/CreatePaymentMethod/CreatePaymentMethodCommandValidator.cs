using FluentValidation;

namespace ErpSystem.Application.PaymentMethods.Commands.CreatePaymentMethod;

public class CreatePaymentMethodCommandValidator : AbstractValidator<CreatePaymentMethodCommand>
{
    public CreatePaymentMethodCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Payment method name is required.")
            .MaximumLength(50)
            .WithMessage("Payment method name must not exceed 50 characters.");
    }
}
