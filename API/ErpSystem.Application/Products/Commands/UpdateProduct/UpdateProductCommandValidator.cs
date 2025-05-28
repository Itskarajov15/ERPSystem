using FluentValidation;

namespace ErpSystem.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(100)
            .WithMessage("Product name must not exceed 100 characters.");
        RuleFor(p => p.Sku)
            .NotEmpty()
            .WithMessage("SKU is required.")
            .MaximumLength(50)
            .WithMessage("SKU must not exceed 50 characters.");
        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters.");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        RuleFor(p => p.ReorderLevel)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Reorder level must be greater than or equal to zero.");
        RuleFor(p => p.UnitOfMeasureId).NotEmpty().WithMessage("Unit of measure ID is required.");
    }
}
