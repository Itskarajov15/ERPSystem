using ErpSystem.Application.Common.Models;
using ErpSystem.Application.Products.DTOs;
using MediatR;

namespace ErpSystem.Application.Products.Queries.GetProducts;

public record GetProductsQuery : IRequest<PaginatedList<ProductDto>>
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? SearchTerm { get; set; }

    public string? SortBy { get; set; }

    public bool SortDescending { get; set; }

    public bool Ascending { get; set; }

    public bool OnlyLowStock { get; set; }
}
