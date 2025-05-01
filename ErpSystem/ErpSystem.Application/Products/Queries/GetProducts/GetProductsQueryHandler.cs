using AutoMapper;
using ErpSystem.Application.Common.Models;
using ErpSystem.Application.Products.DTOs;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Products.Queries.GetProducts;

internal class GetProductsQueryHandler
    : IRequestHandler<GetProductsQuery, PaginatedList<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProductDto>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken
    )
    {
        var (products, totalCount) = await _productRepository.GetProductsAsync(
            request.SearchTerm,
            request.SortBy,
            request.Ascending,
            request.OnlyLowStock,
            request.Page,
            request.PageSize,
            cancellationToken
        );

        var mappedProducts = _mapper.Map<List<ProductDto>>(products);

        var paginatedList = new PaginatedList<ProductDto>(
            mappedProducts,
            totalCount,
            request.Page,
            request.PageSize
        );

        return paginatedList;
    }
}
