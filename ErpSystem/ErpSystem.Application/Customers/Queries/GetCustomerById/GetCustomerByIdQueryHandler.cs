using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Application.Customers.DTOs;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Customers.Queries.GetCustomerById;

internal class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    private readonly IRepository _repository;

    public GetCustomerByIdQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<CustomerDto> Handle(
        GetCustomerByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var customer = await _repository.GetByIdAsync<Customer>(request.Id);

        if (customer is null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        return new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Address = customer.Address,
            Phone = customer.Phone,
            Email = customer.Email,
            ContactName = customer.ContactName,
        };
    }
}
