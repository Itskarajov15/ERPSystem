using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces.Repositories;

namespace ErpSystem.Infrastructure.Persistance.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context)
        : base(context) { }
}
