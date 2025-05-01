using MediatR;

namespace ErpSystem.Application.Suppliers.Commands.AddSupplier;

public record AddSupplierCommand(
    string Name,
    string Address,
    string Phone,
    string Email,
    string ContactPerson
) : IRequest<Guid>;
