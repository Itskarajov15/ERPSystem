using FluentValidation;

namespace ErpSystem.Application.Products.Queries.GetProducts;

public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(x => x.PaginationParams)
            .NotNull()
            .WithMessage("Pagination parameters cannot be null.");
        RuleFor(x => x.PaginationParams.Page)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0.");
        RuleFor(x => x.PaginationParams.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0.");
    }
}
