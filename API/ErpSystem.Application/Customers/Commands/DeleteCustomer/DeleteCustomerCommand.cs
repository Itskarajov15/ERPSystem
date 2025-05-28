using MediatR;

namespace ErpSystem.Application.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(Guid Id) : IRequest;
