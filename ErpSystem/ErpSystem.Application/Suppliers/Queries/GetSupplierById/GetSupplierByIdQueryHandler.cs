using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Application.Suppliers.DTOs;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Suppliers.Queries.GetSupplierById;

internal class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, SupplierDto>
{
    private readonly IRepository _repository;

    public GetSupplierByIdQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<SupplierDto> Handle(
        GetSupplierByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var supplier = await _repository.GetByIdAsync<Supplier>(request.Id);

        if (supplier == null)
        {
            throw new NotFoundException(nameof(Supplier), request.Id);
        }

        return new SupplierDto()
        {
            Id = supplier.Id,
            Address = supplier.Address,
            ContactName = supplier.ContactPerson,
            Email = supplier.Email,
            Name = supplier.Name,
            PhoneNumber = supplier.Phone,
        };
    }
}
