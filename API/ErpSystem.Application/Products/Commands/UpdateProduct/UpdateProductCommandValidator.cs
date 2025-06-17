using ErpSystem.Application.Common.Constants;
using FluentValidation;

namespace ErpSystem.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage(ProductErrorKeys.ProductNotFound);
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage(ProductErrorKeys.NameRequired)
            .MaximumLength(100)
            .WithMessage(ProductErrorKeys.NameTooLong);
        RuleFor(p => p.Sku)
            .NotEmpty()
            .WithMessage(ProductErrorKeys.SkuRequired)
            .MaximumLength(50)
            .WithMessage(ProductErrorKeys.SkuTooLong);
        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage(ProductErrorKeys.DescriptionRequired)
            .MaximumLength(500)
            .WithMessage(ProductErrorKeys.DescriptionTooLong);
        RuleFor(p => p.UnitPrice)
            .GreaterThan(0)
            .WithMessage(ProductErrorKeys.UnitPriceGreaterThanZero);
        RuleFor(p => p.ReorderLevel)
            .GreaterThanOrEqualTo(0)
            .WithMessage(ProductErrorKeys.ReorderLevelGreaterThanOrEqualToZero);
        RuleFor(p => p.UnitOfMeasureId)
            .NotEmpty()
            .WithMessage(ProductErrorKeys.UnitOfMeasureRequired);
    }
}
