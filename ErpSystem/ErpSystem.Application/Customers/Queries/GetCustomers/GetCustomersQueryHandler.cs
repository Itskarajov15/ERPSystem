using ErpSystem.Application.Customers.DTOs;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Customers.Queries.GetCustomers;

internal class GetCustomersQueryHandler
    : IRequestHandler<GetCustomersQuery, PageResult<CustomerDto>>
{
    private readonly IRepository _repository;

    public GetCustomersQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageResult<CustomerDto>> Handle(
        GetCustomersQuery request,
        CancellationToken cancellationToken
    )
    {
        var filterBy = ComposeFilterBy(request.CustomerFilters);

        var customers = await _repository.GetPaginatedAsync<Customer, CustomerDto>(
            request.PaginationParams,
            q =>
                q.Select(c => new CustomerDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Phone = c.Phone,
                    Email = c.Email,
                    ContactName = c.ContactName,
                }),
            filterBy
        );

        return customers;
    }

    private Func<IQueryable<Customer>, IQueryable<Customer>> ComposeFilterBy(
        CustomerFilters? customerFilters
    ) =>
        query =>
        {
            if (customerFilters == null)
            {
                return query;
            }

            if (customerFilters.SearchTerm != null)
            {
                var searchTermLowerCase = customerFilters.SearchTerm.ToLower();

                query = query.Where(s =>
                    s.Name.ToLower().Contains(searchTermLowerCase)
                    || s.Phone.Contains(searchTermLowerCase)
                    || s.ContactName.ToLower().Contains(searchTermLowerCase)
                    || s.Address.ToLower().Contains(searchTermLowerCase)
                    || s.Email.ToLower().Contains(searchTermLowerCase)
                );
            }

            return query;
        };
}
