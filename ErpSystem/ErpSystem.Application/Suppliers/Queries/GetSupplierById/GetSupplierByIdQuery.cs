using ErpSystem.Application.Suppliers.DTOs;
using MediatR;

namespace ErpSystem.Application.Suppliers.Queries.GetSupplierById;

public record GetSupplierByIdQuery(Guid Id) : IRequest<SupplierDto>;
