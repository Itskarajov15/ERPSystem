using ErpSystem.Application.Common.Constants;
using FluentValidation;

namespace ErpSystem.Application.Orders.Commands.AddOrder;

public class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
{
    public AddOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage(CustomerErrorKeys.CustomerRequired);

        RuleFor(x => x.PaymentMethodId)
            .NotEmpty()
            .WithMessage(PaymentMethodErrorKeys.PaymentMethodRequired);

        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage(OrderErrorKeys.OrderItemsRequired)
            .Must(items => items.All(item => item.Quantity > 0))
            .WithMessage(ProductErrorKeys.QuantityInvalid);

        RuleForEach(x => x.Items)
            .ChildRules(items =>
            {
                items
                    .RuleFor(i => i.ProductId)
                    .NotEmpty()
                    .WithMessage(ProductErrorKeys.ProductRequired);

                items
                    .RuleFor(i => i.Quantity)
                    .GreaterThan(0)
                    .WithMessage(ProductErrorKeys.QuantityInvalid);

                items
                    .RuleFor(i => i.UnitPrice)
                    .GreaterThan(0)
                    .WithMessage(ProductErrorKeys.UnitPriceInvalid);
            });
    }
}
