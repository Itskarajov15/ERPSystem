using ErpSystem.Application.Invoices.DTOs;

namespace ErpSystem.Application.Common.Interfaces;

public interface IPdfService
{
    Task<byte[]> GenerateInvoicePdfAsync(InvoiceDetailDto invoice);
}
