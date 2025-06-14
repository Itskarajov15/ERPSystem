﻿using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Entities.Sales;

namespace ErpSystem.Domain.Entities.Inventory;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Sku { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Quantity { get; set; }

    public int ReservedQuantity { get; set; }

    public decimal Price { get; set; }

    public int ReorderLevel { get; set; }

    public Guid UnitOfMeasureId { get; set; }

    public UnitOfMeasure UnitOfMeasure { get; set; } = null!;

    public ICollection<DeliveryItem> DeliveryItems { get; set; } = new HashSet<DeliveryItem>();

    public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
}
