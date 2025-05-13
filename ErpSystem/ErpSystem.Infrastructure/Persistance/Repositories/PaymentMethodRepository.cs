using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces.Repositories;

namespace ErpSystem.Infrastructure.Persistance.Repositories;

public class PaymentMethodRepository : Repository<PaymentMethod>, IPaymentMethodRepository
{
    public PaymentMethodRepository(ApplicationDbContext context)
        : base(context) { }
}
