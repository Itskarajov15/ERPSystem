using ErpSystem.Application.Common.Models;
using ErpSystem.Application.Suppliers.DTOs;
using MediatR;

namespace ErpSystem.Application.Suppliers.Queries.GetSuppliersByName;

public record GetSuppliersByNameQuery(string Name) : IRequest<PaginatedList<SupplierDto>>;
