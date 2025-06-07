using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Invoices;

namespace ErpSystem.Frontend.Core.Interfaces;

public interface IInvoiceService
{
    Task<PageResult<InvoiceViewModel>> GetInvoicesAsync(InvoiceFilterModel? filter = null);
    Task<InvoiceDetailViewModel?> GetInvoiceByIdAsync(Guid id);
    Task<Guid> CreateInvoiceFromOrderAsync(Guid orderId, string? notes = null);
    Task UpdateInvoiceStatusAsync(Guid invoiceId, InvoiceStatus status);
    Task<byte[]> GetInvoicePdfAsync(Guid invoiceId);
}
