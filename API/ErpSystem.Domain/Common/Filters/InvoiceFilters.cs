using ErpSystem.Domain.Entities.Financial;

namespace ErpSystem.Domain.Common.Filters;

public class InvoiceFilters
{
    public string? SearchTerm { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public InvoiceStatus? Status { get; set; }
}
