using System.ComponentModel.DataAnnotations;
using ErpSystem.Frontend.Core.Models.Common;

namespace ErpSystem.Frontend.Core.Models.Orders;

public class OrderFilterModel : PaginationParams
{
    [Display(Name = "Статус")]
    public string? Status { get; set; }

    [Display(Name = "Клиент")]
    public Guid? CustomerId { get; set; }

    [Display(Name = "От дата")]
    [DataType(DataType.Date)]
    public DateTime? FromDate { get; set; }

    [Display(Name = "До дата")]
    [DataType(DataType.Date)]
    public DateTime? ToDate { get; set; }
}
