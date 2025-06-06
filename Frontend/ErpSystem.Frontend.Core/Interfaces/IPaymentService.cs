using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Payments;

namespace ErpSystem.Frontend.Core.Interfaces;

public interface IPaymentService
{
    Task<PageResult<PaymentViewModel>> GetPaymentsAsync(PaymentFilterModel? filter = null);
    Task<PaymentDetailViewModel?> GetPaymentByIdAsync(Guid paymentId);
    Task RecordPaymentAsync(RecordPaymentRequest request);
} 