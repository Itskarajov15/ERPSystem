using System.ComponentModel.DataAnnotations;
using ErpSystem.Frontend.Core.Models.Common;

namespace ErpSystem.Frontend.Core.Models.Invoices;

public class InvoiceFilterModel : PaginationParams
{
    [Display(Name = "Търсене")]
    public string? SearchTerm { get; set; }

    [Display(Name = "Статус")]
    public InvoiceStatus? Status { get; set; }

    [Display(Name = "Клиент")]
    public Guid? CustomerId { get; set; }

    [Display(Name = "От дата")]
    [DataType(DataType.Date)]
    public DateTime? FromDate { get; set; }

    [Display(Name = "До дата")]
    [DataType(DataType.Date)]
    public DateTime? ToDate { get; set; }
}
