using AutoMapper;
using ErpSystem.Application.Common.Models;
using ErpSystem.Application.Suppliers.DTOs;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Suppliers.Queries.GetSuppliersByName;

internal class GetSuppliersByNameQueryHandler
    : IRequestHandler<GetSuppliersByNameQuery, PaginatedList<SupplierDto>>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;

    public GetSuppliersByNameQueryHandler(ISupplierRepository supplierRepository, IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<SupplierDto>> Handle(
        GetSuppliersByNameQuery request,
        CancellationToken cancellationToken
    )
    {
        var suppliers = await _supplierRepository.GetSuppliersByNameAsync(
            request.Name,
            cancellationToken
        );

        var suppliersDto = _mapper.Map<List<SupplierDto>>(suppliers);

        return new PaginatedList<SupplierDto>(suppliersDto, suppliersDto.Count, 1, 10);
    }
}
