﻿namespace ErpSystem.Application.Deliveries.DTOs;

public abstract class BaseDeliveryItemDto
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}
