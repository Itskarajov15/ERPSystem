using MediatR;

namespace ErpSystem.Application.Suppliers.Commands.DeleteSupplier;

public record DeleteSupplierCommand(Guid Id) : IRequest;
