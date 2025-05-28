using MediatR;

namespace ErpSystem.Application.Customers.Commands.AddCustomer;

public record AddCustomerCommand(
    string Name,
    string ContactName,
    string Phone,
    string Email,
    string Address
) : IRequest<Guid>;
