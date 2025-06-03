using ErpSystem.Application.Common.Constants;
using FluentValidation;

namespace ErpSystem.Application.Deliveries.Commands.AddDelivery;

public class AddDeliveryCommandValidator : AbstractValidator<AddDeliveryCommand>
{
    public AddDeliveryCommandValidator()
    {
        RuleFor(x => x.SupplierId).NotEmpty().WithMessage(SupplierErrorKeys.SupplierRequired);

        RuleFor(x => x.DeliveryNumber)
            .NotEmpty()
            .WithMessage(DeliveryErrorKeys.DeliveryNumberRequired)
            .MaximumLength(50)
            .WithMessage(DeliveryErrorKeys.DeliveryNumberTooLong);

        RuleFor(x => x.DeliveryDate).NotEmpty().WithMessage(DeliveryErrorKeys.DeliveryDateRequired);

        RuleFor(x => x.Items)
            .NotNull()
            .WithMessage(DeliveryErrorKeys.DeliveryItemsRequired)
            .NotEmpty()
            .WithMessage(DeliveryErrorKeys.DeliveryItemsRequired)
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
