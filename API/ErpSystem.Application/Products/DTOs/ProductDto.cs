namespace ErpSystem.Application.Products.DTOs;

public class ProductDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Sku { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal UnitPrice { get; set; }

    public Guid UnitOfMeasureId { get; set; }

    public string UnitOfMeasureName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public int ReservedQuantity { get; set; }

    public int ReorderLevel { get; set; }

    public bool IsLowStock => Quantity < ReorderLevel;
}
