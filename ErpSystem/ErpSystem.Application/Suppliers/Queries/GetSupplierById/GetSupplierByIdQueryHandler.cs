using AutoMapper;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Application.Suppliers.DTOs;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Suppliers.Queries.GetSupplierById;

internal class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, SupplierDto>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;

    public GetSupplierByIdQueryHandler(ISupplierRepository supplierRepository, IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }

    public async Task<SupplierDto> Handle(
        GetSupplierByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var supplier = await _supplierRepository.GetByIdAsync(request.Id, cancellationToken);

        if (supplier == null)
        {
            throw new NotFoundException(nameof(Supplier), request.Id);
        }

        var mappedSupplier = _mapper.Map<SupplierDto>(supplier);

        return mappedSupplier;
    }
}
