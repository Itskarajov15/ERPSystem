using FluentValidation;

namespace ErpSystem.Application.Orders.Commands.AddOrder;

public class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
{
    public AddOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(x => x.PaymentMethodId).NotEmpty().WithMessage("Payment method ID is required.");

        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("At least one order item is required.")
            .Must(items => items.All(item => item.Quantity > 0))
            .WithMessage("All items must have a quantity greater than zero.");

        RuleForEach(x => x.Items)
            .ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId).NotEmpty().WithMessage("Product ID is required.");

                items
                    .RuleFor(i => i.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than zero.");

                items
                    .RuleFor(i => i.UnitPrice)
                    .GreaterThan(0)
                    .WithMessage("Unit price must be greater than zero.");
            });
    }
}
