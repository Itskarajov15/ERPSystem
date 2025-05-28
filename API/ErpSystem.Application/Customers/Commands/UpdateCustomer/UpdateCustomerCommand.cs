using MediatR;

namespace ErpSystem.Application.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand(
    Guid Id,
    string Name,
    string Address,
    string Phone,
    string Email,
    string ContactName
) : IRequest;
