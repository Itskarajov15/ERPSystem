using MediatR;

namespace ErpSystem.Application.Suppliers.Commands.UpdateSupplier;

public record UpdateSupplierCommand(
    Guid Id,
    string Name,
    string Address,
    string Phone,
    string Email,
    string ContactPerson
) : IRequest;
