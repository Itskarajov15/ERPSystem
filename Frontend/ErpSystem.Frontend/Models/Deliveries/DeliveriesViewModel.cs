using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSystem.Frontend.Web.Models.Deliveries;

public class DeliveriesViewModel
{
    public List<DeliveryViewModel> Deliveries { get; set; } = new();
    public DeliveryFilterModel Filter { get; set; } = new();
    public int TotalCount { get; set; }
    public List<SelectListItem> Suppliers { get; set; } = new();
    public List<SelectListItem> Products { get; set; } = new();
    public string? ErrorMessage { get; set; }
} 