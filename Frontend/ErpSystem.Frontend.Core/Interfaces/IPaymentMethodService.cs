using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.PaymentMethods;

namespace ErpSystem.Frontend.Core.Interfaces;

public interface IPaymentMethodService
{
    Task<PageResult<PaymentMethodViewModel>> GetPaymentMethodsAsync(PaymentMethodFilterModel? filter = null);
    Task<List<PaymentMethodViewModel>> GetAllPaymentMethodsAsync();
    Task<PaymentMethodViewModel?> GetPaymentMethodByIdAsync(Guid id);
    Task<Guid> AddPaymentMethodAsync(PaymentMethodEditModel model);
    Task UpdatePaymentMethodAsync(PaymentMethodEditModel model);
    Task DeletePaymentMethodAsync(Guid id);
} 