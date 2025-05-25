using ErpSystem.Application.Customers.DTOs;
using MediatR;

namespace ErpSystem.Application.Customers.Queries.GetCustomerById;

public record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerDto>;
