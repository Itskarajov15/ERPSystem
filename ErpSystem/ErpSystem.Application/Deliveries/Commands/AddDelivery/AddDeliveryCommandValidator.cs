using FluentValidation;

namespace ErpSystem.Application.Deliveries.Commands.AddDelivery;

public class AddDeliveryCommandValidator : AbstractValidator<AddDeliveryCommand>
{
    public AddDeliveryCommandValidator()
    {
        RuleFor(x => x.SupplierId).NotEmpty().WithMessage("Supplier ID is required.");
        RuleFor(x => x.DeliveryDate)
            .NotEmpty()
            .WithMessage("Delivery date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Delivery date cannot be in the future.");
        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("At least one delivery item is required.")
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
