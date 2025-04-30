using FluentValidation;

namespace ErpSystem.Application.Products.Queries.GetProducts;

public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0).WithMessage("Page number must be greater than 0");

        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page size must bre greater than 0");

        RuleFor(x => x.SearchTerm)
            .MaximumLength(100)
            .WithMessage("Search term cannot be longer than 100 characters.");

        RuleFor(x => x.SortBy)
            .MaximumLength(100)
            .WithMessage("Sort by cannot be longer that 100 characters");
    }
}
