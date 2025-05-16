using ErpSystem.Application.Suppliers.DTOs;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Suppliers.Queries.GetSuppliers;

internal class GetSuppliersQueryHandler
    : IRequestHandler<GetSuppliersQuery, PageResult<SupplierDto>>
{
    private readonly IRepository _repository;

    public GetSuppliersQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageResult<SupplierDto>> Handle(
        GetSuppliersQuery request,
        CancellationToken cancellationToken
    )
    {
        var filterBy = ComposeFilterBy(request.SupplierFilters);

        var result = await _repository.GetPaginatedAsync(
            request.PaginationParams,
            q =>
                q.Select(s => new SupplierDto()
                {
                    Id = s.Id,
                    Address = s.Address,
                    ContactName = s.ContactPerson,
                    CreatedAt = s.CreatedAt.ToString("dd/MM/yyyy"),
                    Email = s.Email,
                    Name = s.Name,
                    PhoneNumber = s.Phone,
                    UpdatedAt = s.LastModifiedAt.HasValue
                        ? s.LastModifiedAt.Value.ToString("dd/MM/yyyy")
                        : null,
                }),
            filterBy
        );

        return result;
    }

    private Func<IQueryable<Supplier>, IQueryable<Supplier>> ComposeFilterBy(
        SupplierFilters? supplierFilters
    ) =>
        query =>
        {
            if (supplierFilters == null)
            {
                return query;
            }

            if (supplierFilters.SearchTerm != null)
            {
                var searchTermLowerCase = supplierFilters.SearchTerm.ToLower();

                query = query.Where(s =>
                    s.Name.ToLower().Contains(searchTermLowerCase)
                    || s.Phone.Contains(searchTermLowerCase)
                    || s.ContactPerson.ToLower().Contains(searchTermLowerCase)
                    || s.Address.ToLower().Contains(searchTermLowerCase)
                    || s.Email.ToLower().Contains(searchTermLowerCase)
                );
            }

            return query;
        };
}
